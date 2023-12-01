using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform barrelT;

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float maxDamage;

    [SerializeField] private float shootCooldownTime;
    [SerializeField] private float minShootCooldownTime;

    [SerializeField] private GameObject shootParticles;

    private bool onCooldown = false;


    private AudioSource source;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private Slider shootSlider;

    private Ray ray;
    private LineRenderer bulletLine;




    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        bulletLine = GetComponent<LineRenderer>();
        shootSlider.value = shootSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }





    private void Shoot()
    {
        if (GameManager.gameMan.gameOver || GameManager.gameMan.paused) return;
        if (onCooldown) return;
        onCooldown = true;
        ray.origin = barrelT.position;
        ray.direction = barrelT.forward;

        bulletLine.SetPosition(0, ray.origin);

        bulletLine.enabled = true;
        source.PlayOneShot(shootSound);
        StartCoroutine(DisableEffects());
        StartCoroutine(ShotCooldown());

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            bulletLine.SetPosition(1, hit.point);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
                Instantiate(shootParticles, hit.point, Quaternion.identity);
            }
        }
        else
        {
            bulletLine.SetPosition(1, ray.origin + ray.direction * range);
        }
    }


    private IEnumerator DisableEffects()
    {
        yield return new WaitForSeconds(0.1f);
        bulletLine.enabled = false;
    }

    private IEnumerator ShotCooldown()
    {
        float timer = 0;
        while (timer < shootCooldownTime)
        {
            shootSlider.value = timer / shootCooldownTime;
            timer += Time.deltaTime;
            yield return null;
        }
        onCooldown = false;
        shootSlider.value = shootSlider.maxValue;
    }




    public void ChangeCooldown(float timeReduction)
    {
        shootCooldownTime -= timeReduction;
        if (shootCooldownTime < minShootCooldownTime) shootCooldownTime = minShootCooldownTime;
    }


    public void ChangeDamage(float addedDamage)
    {
        damage += addedDamage;
        if (damage > maxDamage) damage = maxDamage;
    }

}
