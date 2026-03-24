using UnityEngine;

public class box : MonoBehaviour
{
    public bool siparis;
    public int sipariTip = 0;
    Animator animator;
    hands hands;
    private void Start()
    {
        hands = FindObjectOfType<hands>();  
        animator = GetComponent<Animator>();
    }

    public void siparisAl()
    {

        if (!hands.siparis && siparis)
        {
            hands.interact();
            myMath.waitAndStart(0.3f, () => {
                hands.siparisAl(true,sipariTip);
                siparis = false;
                animator.SetTrigger("take");
            });

            
        }
        else if(hands.siparis)
        {
            sipariTip = hands.siparisAl(false, sipariTip);
            siparis = true;
            animator.SetTrigger("take");
            myMath.waitAndStart(0.3f, () => {
                hands.interact();
            });
        }
    }
}

