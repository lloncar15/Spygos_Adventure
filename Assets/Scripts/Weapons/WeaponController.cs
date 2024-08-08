using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int ammo = 0;
    public int dmg = 1;
    public GameObject saxBulletPrefab;
    public Transform saxBulletSpawnPoint;
    public float cooldown = 1.0f;
    public float lastShotTime = 0.0f;

    public int index = 0;

    public ScoreManager scoreManager;

    public void Awake()
    {
        if (index != -1 && GameController.Instance != null) {
            ammo = GameController.Instance.ammos[index];
        }
    }

    public void Start()
    {
        if (scoreManager != null) 
        {
            scoreManager.UpdateAmmo();
        }
    }

    void Update()
    {
        lastShotTime += Time.deltaTime;
    }


    public void Shoot(bool isFlipped)
    {
            GameObject bullet = Instantiate(saxBulletPrefab, saxBulletSpawnPoint.position, saxBulletSpawnPoint.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.dmg = dmg;
            bulletController.isFlipped = isFlipped;
            --ammo;
            lastShotTime = 0.0f;
            if (scoreManager != null) {
                scoreManager.UpdateAmmo();
            }
    }

    public bool CanShoot()
    {
        return lastShotTime > cooldown && ammo > 0;
    }

    public void ShootFlower()
    {
        --ammo;
        lastShotTime = 0.0f;
        scoreManager.UpdateAmmo();
    }
}
