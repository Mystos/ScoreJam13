using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerActionControls playerActionControls;
    [SerializeField] private Animator attack;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Vector2 attackAreaSize;
    public Transform attackAreaPos;

    public int attackDamage = 4;

    public LayerMask attackCollider;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            // Attack
            if (playerActionControls.Player.Attack.triggered)
            {
                Collider2D[] ennemies = Physics2D.OverlapCapsuleAll(attackAreaPos.position, attackAreaSize,CapsuleDirection2D.Vertical, attackCollider);
                Debug.Log(ennemies);
                foreach (Collider2D item in ennemies)
                {
                    if (item.gameObject.CompareTag("Enemy"))
                    {
                        item.GetComponentInParent<GenericHealth>()?.Hit(attackDamage);
                        item.GetComponentInParent<Knockback>()?.AddImpact(-item.gameObject.transform.up, 50);
                    }
                }
                attack.Play("PlayerAttackFX");
                Debug.Log("attack");
            }

            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }


    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    private void OnDrawGizmos()
    {
         DrawWireCapsule(attackAreaPos.position, attackAreaPos.rotation, attackAreaSize.x, attackAreaSize.y, Color.red);
    }

    public static void DrawWireCapsule(Vector3 _pos, Quaternion _rot, float _radius, float _height, Color _color = default(Color))
    {
        if (_color != default(Color))
            Handles.color = _color;
        Matrix4x4 angleMatrix = Matrix4x4.TRS(_pos, _rot, Handles.matrix.lossyScale);
        using (new Handles.DrawingScope(angleMatrix))
        {
            var pointOffset = (_height - (_radius * 2)) / 2;

            //draw sideways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, _radius);
            Handles.DrawLine(new Vector3(0, pointOffset, -_radius), new Vector3(0, -pointOffset, -_radius));
            Handles.DrawLine(new Vector3(0, pointOffset, _radius), new Vector3(0, -pointOffset, _radius));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, _radius);
            //draw frontways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, _radius);
            Handles.DrawLine(new Vector3(-_radius, pointOffset, 0), new Vector3(-_radius, -pointOffset, 0));
            Handles.DrawLine(new Vector3(_radius, pointOffset, 0), new Vector3(_radius, -pointOffset, 0));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, _radius);
            //draw center
            Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, _radius);
            Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, _radius);

        }
    }
}
