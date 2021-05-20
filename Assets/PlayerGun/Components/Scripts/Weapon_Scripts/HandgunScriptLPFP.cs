using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Leap;

// ----- Low Poly FPS Pack Free Version -----
public class HandgunScriptLPFP : MonoBehaviour
{
    private string currentSceneName;

    //Animator component attached to weapon
    Animator anim;

    [Header("Gun Camera")]
    //Main gun camera
    public Camera gunCamera;

    [Header("Gun Camera Options")]
    //How fast the camera field of view changes when aiming 
    [Tooltip("How fast the camera field of view changes when aiming.")]
    public float fovSpeed = 15.0f;
    //Default camera field of view
    [Tooltip("Default value for camera field of view (40 is recommended).")]
    public float defaultFov = 40.0f;

    public float aimFov = 15.0f;

    [Header("UI Weapon Name")]
    [Tooltip("Name of the current weapon, shown in the game UI.")]
    public string weaponName;
    private string storedWeaponName;

    [Header("Weapon Sway")]
    //Enables weapon sway
    [Tooltip("Toggle weapon sway.")]
    public bool weaponSway;

    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothValue = 4.0f;

    private Vector3 initialSwayPosition;

    [Header("Weapon Settings")]

    public float sliderBackTimer = 1.58f;
    private bool hasStartedSliderBack;

    //Eanbles auto reloading when out of ammo
    [Tooltip("Enables auto reloading when out of ammo.")]
    public bool autoReload;
    //Delay between shooting last bullet and reloading
    public float autoReloadDelay;
    //Check if reloading
    private bool isReloading;

    //How much ammo is currently left
    private int currentAmmo;
    //Totalt amount of ammo
    [Tooltip("How much ammo the weapon should have.")]
    public int ammo;
    //Check if out of ammo
    private bool outOfAmmo;

    [Header("Bullet Settings")]
    //Bullet
    [Tooltip("How much force is applied to the bullet when shooting.")]
    public float bulletForce = 400;
    [Tooltip("How long after reloading that the bullet model becomes visible " +
        "again, only used for out of ammo reload aniamtions.")]
    public float showBulletInMagDelay = 0.6f;
    [Tooltip("The bullet model inside the mag, not used for all weapons.")]
    public SkinnedMeshRenderer bulletInMagRenderer;

    [Header("BulletHole Settings")]
    public Sprite[] glassDecals;
    public Sprite[] woodDecals;
    public GameObject bulletHolePrefab;

    [Header("BulletSpark Settings")]
    public GameObject metalSparkEffect;
    public GameObject sandSparkEffect;
    public GameObject woodSparkEffect;

    [Header("Grenade Settings")]
    public float grenadeSpawnDelay = 0.35f;

    [Header("Muzzleflash Settings")]
    public bool randomMuzzleflash = false;
    //min should always bee 1
    private int minRandomValue = 1;

    [Range(2, 25)]
    public int maxRandomValue = 5;

    private int randomMuzzleflashValue;

    public bool enableMuzzleflash = true;
    public ParticleSystem muzzleParticles;
    public bool enableSparks = true;
    public ParticleSystem sparkParticles;
    public int minSparkEmission = 1;
    public int maxSparkEmission = 7;

    [Header("Muzzleflash Light Settings")]
    public Light muzzleflashLight;
    public float lightDuration = 0.02f;

    [Header("Audio Source")]
    //Main audio source
    public AudioSource mainAudioSource;
    //Audio source used for shoot sound
    public AudioSource shootAudioSource;

    [Header("UI Components")]
    public Text timescaleText;
    public Text currentWeaponText;
    public Text currentAmmoText;
    public Text totalAmmoText;
    public GameObject ReloadinfoUI;

    [System.Serializable]
    public class prefabs
    {
        [Header("Prefabs")]
        public Transform bulletPrefab;
        public Transform casingPrefab;
        public Transform grenadePrefab;
    }
    public prefabs Prefabs;

    [System.Serializable]
    public class spawnpoints
    {
        [Header("Spawnpoints")]
        //Array holding casing spawn points 
        //Casing spawn point array
        public Transform casingSpawnPoint;
        //Bullet prefab spawn from this point
        public Transform bulletSpawnPoint;
        //Grenade prefab spawn from this point
        public Transform grenadeSpawnPoint;
    }
    public spawnpoints Spawnpoints;

    [System.Serializable]
    public class soundClips
    {
        public AudioClip shootSound;
        public AudioClip takeOutSound;
        public AudioClip holsterSound;
        public AudioClip reloadSoundOutOfAmmo;
        public AudioClip reloadSoundAmmoLeft;
        public AudioClip aimSound;
    }
    public soundClips SoundClips;

    /* ---- Leap Motion Action ---- */
    private bool _isShoot = false;
    private bool _isGrenade = false;
    private bool _isLoading = false;

    private bool _isShootDown = false;
    private bool _isGrenadeDown = false;

    private float _ShootDelay = 0.2f;
    private float _GrenadeDelay = 5.0f;
    /* ---------------------------- */

    private void Awake()
    {
        //Set the animator component
        anim = GetComponent<Animator>();
        //Set current ammo to total ammo value
        currentAmmo = ammo;

        muzzleflashLight.enabled = false;
    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        ReloadinfoUI.SetActive(false);
        //Save the weapon name
        storedWeaponName = weaponName;
        //Get weapon name from string to text
        currentWeaponText.text = weaponName;
        //Set total ammo text from total ammo int
        totalAmmoText.text = ammo.ToString();

        //Weapon sway
        initialSwayPosition = transform.localPosition;

        //Set the shoot sound to audio source
        shootAudioSource.clip = SoundClips.shootSound;
    }


    private void Update()
    {
        if (FindObjectOfType<GameManager>().isPlayerWinFunRuned
            || FindObjectOfType<GameManager>().isPlayerLoseFunRuned) return;

        _isShoot = FindObjectOfType<GameManager>().isShoot;
        _isGrenade = FindObjectOfType<GameManager>().isGrenade;
        _isLoading = FindObjectOfType<GameManager>().isLoading;

        if (!isReloading)
        {
            gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,
                aimFov, fovSpeed * Time.deltaTime);

            anim.SetBool("Aim", true);
        }

        //If randomize muzzleflash is true, genereate random int values
        if (randomMuzzleflash == true)
        {
            randomMuzzleflashValue = Random.Range(minRandomValue, maxRandomValue);
        }

        //Set current ammo text from ammo int
        currentAmmoText.text = currentAmmo.ToString();

        //Continosuly check which animation 
        //is currently playing
        AnimationCheck();

        checkAction();
        checkAmmo();
    }

    private void LateUpdate()
    {
        //Weapon sway
        if (weaponSway == true)
        {
            float movementX = -Input.GetAxis("Mouse X") * swayAmount;
            float movementY = -Input.GetAxis("Mouse Y") * swayAmount;
            //Clamp movement to min and max values
            movementX = Mathf.Clamp
                (movementX, -maxSwayAmount, maxSwayAmount);
            movementY = Mathf.Clamp
                (movementY, -maxSwayAmount, maxSwayAmount);
            //Lerp local pos
            Vector3 finalSwayPosition = new Vector3
                (movementX, movementY, 0);
            transform.localPosition = Vector3.Lerp
                (transform.localPosition, finalSwayPosition +
                initialSwayPosition, Time.deltaTime * swaySmoothValue);
        }
    }

    private void checkAction()
    {
        //Shooting 
        if (!_isShootDown && (Input.GetMouseButtonDown(0) || _isShoot) && !outOfAmmo && !isReloading)
        {
            anim.Play("Aim Fire", 0, 0f);

            //If random muzzle is false
            if (!randomMuzzleflash)
            {
                muzzleParticles.Emit(1);
                //If random muzzle is true
            }
            else if (randomMuzzleflash == true)
            {
                //Only emit if random value is 1
                if (randomMuzzleflashValue == 1)
                {
                    if (enableSparks == true)
                    {
                        //Emit random amount of spark particles
                        sparkParticles.Emit(Random.Range(1, 6));
                    }
                    if (enableMuzzleflash == true)
                    {
                        muzzleParticles.Emit(1);
                        //Light flash start
                        StartCoroutine(MuzzleFlashLight());
                    }
                }
            }

            //Remove 1 bullet from ammo
            currentAmmo -= 1;

            shootAudioSource.clip = SoundClips.shootSound;
            shootAudioSource.Play();

            //Light flash start
            StartCoroutine(MuzzleFlashLight());

            makeBullet();

            _isShootDown = true;

            /* Cool Time */
            StartCoroutine(shootTimer());
        }
        //Throw grenade when pressing G key
        else if (!_isGrenadeDown && (Input.GetKeyDown(KeyCode.G) || _isGrenade))
        {
            StartCoroutine(GrenadeSpawnDelay());
            //Play grenade throw animation
            anim.Play("GrenadeThrow", 0, 0.0f);
            _isGrenadeDown = true;

            /* Cool Time */
            StartCoroutine(grenadeTimer());
        }
        //Reload 
        else if (Input.GetKeyDown(KeyCode.R) || _isLoading)
        {
            //Reload
            Reload();

            if (!hasStartedSliderBack)
            {
                hasStartedSliderBack = true;
                StartCoroutine(HandgunSliderBackDelay());
            }
        }

        _isShoot = false; _isGrenade = false; _isLoading = false;
    }

    private void checkAmmo()
    {
        //If out of ammo
        if (currentAmmo == 0)
        {
            //Show out of ammo text
            currentWeaponText.text = "OUT OF AMMO";
            //Toggle bool
            outOfAmmo = true;
            //Auto reload if true
            if (autoReload == true && !isReloading)
            {
                StartCoroutine(AutoReload());
            }

            //Set slider back
            anim.SetBool("Out Of Ammo Slider", true);
            //Increase layer weight for blending to slider back pose
            anim.SetLayerWeight(1, 1.0f);
        }
        else
        {
            //When ammo is full, show weapon name again
            currentWeaponText.text = storedWeaponName.ToString();
            //Toggle bool
            outOfAmmo = false;
            //anim.SetBool ("Out Of Ammo", false);
            anim.SetLayerWeight(1, 0.0f);
        }
    }

    private void makeBullet()
    {
        //Spawn bullet at bullet spawnpoint
        var bullet = (Transform)Instantiate(
            Prefabs.bulletPrefab,
            Spawnpoints.bulletSpawnPoint.transform.position,
            Spawnpoints.bulletSpawnPoint.transform.rotation);

        //Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity =
        bullet.transform.forward * bulletForce;

        //Spawn casing prefab at spawnpoint
        Instantiate(Prefabs.casingPrefab,
            Spawnpoints.casingSpawnPoint.transform.position,
            Spawnpoints.casingSpawnPoint.transform.rotation);


        Vector3 impactPoint = CastRay();
        if (impactPoint != Vector3.zero) bullet.transform.LookAt(impactPoint);
    }

    private Vector3 CastRay()
    {
        RaycastHit hit;
        int exceptBulletlayerMask = (-1) - (1 << LayerMask.NameToLayer("Bullet")) - (1 << LayerMask.NameToLayer("Player"));  // Everything에서 Bullet 레이어만 제외하고 충돌 체크함

        // Check forward from Player's position except Bullet
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, exceptBulletlayerMask))
        {

            GameObject bulletHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.0001f, Quaternion.identity);

            // BulletHole appear in the direction of the object.
            bulletHole.transform.LookAt(hit.point + hit.normal);

            Debug.Log("hit.transform.tag: " + hit.transform.tag);

            if (hit.transform.tag == "Tables")
            {
                bulletHole.GetComponent<SpriteRenderer>().sprite = woodDecals[UnityEngine.Random.Range(0, woodDecals.Length)];
                StartCoroutine(destroyTimer(bulletHole));
            }
            else if (hit.transform.tag == "Window")
            {
                bool _isBroken = hit.transform.GetComponent<breakWindow>().isBroken;
                if (!_isBroken)
                {
                    bulletHole.GetComponent<SpriteRenderer>().sprite = glassDecals[UnityEngine.Random.Range(0, glassDecals.Length)];
                    StartCoroutine(destroyTimer(bulletHole));
                }
            }
            else if (hit.transform.tag == "Chair")
            {
                Instantiate(metalSparkEffect, hit.point + hit.normal * 0.0001f, Quaternion.identity);
            }
            else if (hit.transform.tag == "Stairs")
            {
                Instantiate(sandSparkEffect, hit.point + hit.normal * 0.0001f, Quaternion.identity);
            }




            return hit.point;
        }

        return Vector3.zero;

    }

    private void movePlayer()
    {
        //Walking when pressing down WASD keys
        if (Input.GetKey(KeyCode.W) &&
            Input.GetKey(KeyCode.A) &&
            Input.GetKey(KeyCode.S) &&
            Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    private IEnumerator shootTimer()
    {
        yield return new WaitForSeconds(_ShootDelay);
        _isShootDown = false;
    }
    private IEnumerator grenadeTimer()
    {
        yield return new WaitForSeconds(_GrenadeDelay);
        _isGrenadeDown = false;
    }

    private IEnumerator destroyTimer(GameObject bulletHole)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(bulletHole);
    }

    private IEnumerator HandgunSliderBackDelay()
    {
        //Wait set amount of time
        yield return new WaitForSeconds(sliderBackTimer);
        //Set slider back
        anim.SetBool("Out Of Ammo Slider", false);
        //Increase layer weight for blending to slider back pose
        anim.SetLayerWeight(1, 0.0f);

        hasStartedSliderBack = false;
    }

    private IEnumerator GrenadeSpawnDelay()
    {
        //Wait for set amount of time before spawning grenade
        yield return new WaitForSeconds(grenadeSpawnDelay);
        //Spawn grenade prefab at spawnpoint
        Instantiate(Prefabs.grenadePrefab,
            Spawnpoints.grenadeSpawnPoint.transform.position,
            Spawnpoints.grenadeSpawnPoint.transform.rotation);
    }

    private IEnumerator AutoReload()
    {
        if (!hasStartedSliderBack)
        {
            hasStartedSliderBack = true;

            StartCoroutine(HandgunSliderBackDelay());
        }
        //Wait for set amount of time
        yield return new WaitForSeconds(autoReloadDelay);

        if (outOfAmmo == true)
        {
            //Play diff anim if out of ammo
            anim.Play("Reload Out Of Ammo", 0, 0f);

            mainAudioSource.clip = SoundClips.reloadSoundOutOfAmmo;
            mainAudioSource.Play();

            //If out of ammo, hide the bullet renderer in the mag
            //Do not show if bullet renderer is not assigned in inspector
            if (bulletInMagRenderer != null)
            {
                bulletInMagRenderer.GetComponent
                <SkinnedMeshRenderer>().enabled = false;
                //Start show bullet delay
                StartCoroutine(ShowBulletInMag());
            }
        }
        //Restore ammo when reloading
        currentAmmo = ammo;
        outOfAmmo = false;
    }

    //Reload
    private void Reload()
    {
        if (currentAmmo < 11)
        {
            if (outOfAmmo == true)
            {
                //Play diff anim if out of ammo
                anim.Play("Reload Out Of Ammo", 0, 0f);

                mainAudioSource.clip = SoundClips.reloadSoundOutOfAmmo;
                mainAudioSource.Play();

                //If out of ammo, hide the bullet renderer in the mag
                //Do not show if bullet renderer is not assigned in inspector
                if (bulletInMagRenderer != null)
                {
                    bulletInMagRenderer.GetComponent
                    <SkinnedMeshRenderer>().enabled = false;
                    //Start show bullet delay
                    StartCoroutine(ShowBulletInMag());
                }
            }
            else
            {
                //Play diff anim if ammo left
                anim.Play("Reload Ammo Left", 0, 0f);

                mainAudioSource.clip = SoundClips.reloadSoundAmmoLeft;
                mainAudioSource.Play();

                //If reloading when ammo left, show bullet in mag
                //Do not show if bullet renderer is not assigned in inspector
                if (bulletInMagRenderer != null)
                {
                    bulletInMagRenderer.GetComponent
                    <SkinnedMeshRenderer>().enabled = true;
                }
            }
            //Restore ammo when reloading
            currentAmmo = ammo;

            outOfAmmo = false;
        }
        else
        {
            ReloadinfoUI.SetActive(true);
            StartCoroutine(destroyUI(ReloadinfoUI));
        }

    }
    public IEnumerator destroyUI(GameObject gameObject)
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    //Enable bullet in mag renderer after set amount of time
    private IEnumerator ShowBulletInMag()
    {
        //Wait set amount of time before showing bullet in mag
        yield return new WaitForSeconds(showBulletInMagDelay);
        bulletInMagRenderer.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    //Show light when shooting, then disable after set amount of time
    private IEnumerator MuzzleFlashLight()
    {
        muzzleflashLight.enabled = true;
        yield return new WaitForSeconds(lightDuration);
        muzzleflashLight.enabled = false;
    }

    //Check current animation playing
    private void AnimationCheck()
    {
        //Check if reloading
        //Check both animations
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Out Of Ammo") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Ammo Left"))
        {
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
    }
}
