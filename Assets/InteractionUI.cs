using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI carryPrompt;
    // Start is called before the first frame update
    private void Update()
    {

        if (!PlayerStats._player.GetComponent<PlayerMovement>().enabled)
        {
            carryPrompt.enabled =false;
            return;
        }// no interaction when in boat

        Collider[] interactables = Physics.OverlapSphere(this.transform.position + this.transform.forward * 1.5f, 1.5f);

        carryPrompt.enabled = true;

        foreach (Collider col in interactables)
        {
            if (col.TryGetComponent<Disembark>(out _))
            {
                continue;
            }

            if (col.TryGetComponent<VeggiePlanter>(out VeggiePlanter planter))
            {
                if (planter._isOccupied) continue;

                carryPrompt.text = "Hold E to Open Seed Menu";
                return;
            }

            if (col.TryGetComponent<Veggie>(out Veggie veggie))
            {
                if (veggie.CurrentState == veggie._harvestableState)
                {
                    carryPrompt.text = "Press E to Harvest";

                }
                else continue;
            }


            if (col.TryGetComponent<Trafficker>(out Trafficker trafficker))
            {
                carryPrompt.enabled = true;
                carryPrompt.text = "'Where are the goods?'";
                if (trafficker._veggieContainer.items.Any())
                {
                    carryPrompt.text = $"Press E to Load Van\n Leaving in {trafficker.CurrentSaleCountdown}";

                }


                if (PlayerStats._player.CarriedItem != null && PlayerStats._player.CarriedItem.TryGetComponent<Veggie>(out _))
                {
                    carryPrompt.text = "Press E to Load Van";
                    return;
                }

                if (PlayerStats._player.CarriedItem != null && PlayerStats._player.CarriedItem.TryGetComponent<Container>(out _))
                {
                    carryPrompt.text = "'Take 'em outta the crate first, bub!'";
                    return;
                }
                return;
            }


            if (col.TryGetComponent<Container>(out Container cont))
            {
                carryPrompt.text = "Press F to Pick Up Crate";
                carryPrompt.enabled = true;
                if (PlayerStats._player.CarriedItem != null && PlayerStats._player.CarriedItem.TryGetComponent<Veggie>(out _))
                {
                    carryPrompt.text = "Press F to Pick Up Crate \n Press E to Stock Veggie";

                }
                else if (cont.items.Any())
                {
                    carryPrompt.text = "Press F to Pick Up Crate \n Press E to Take Out Veggie";

                }


                return;


            }

            if (col.TryGetComponent<BoatInteract>(out BoatInteract boat))
            {
                carryPrompt.enabled = true;

                carryPrompt.text = "Press E to Enter Boat";

                if (PlayerStats._player.CarriedItem != null && PlayerStats._player.CarriedItem.TryGetComponent<Container>(out _))
                {
                    carryPrompt.text = "Press E to Load Crate";
                    return;
                }
                if (PlayerStats._player.CarriedItem != null && PlayerStats._player.CarriedItem.TryGetComponent<Soil>(out _))
                {
                    carryPrompt.text = "Press E to Load Soil";
                    return;
                }

                if (boat.CrateCount.Any())
                {
                    carryPrompt.text = "Press E to Enter Boat" + "\n Press F to Take Out Crate";
                    return;
                }

                return;
            }

            if (col.TryGetComponent<Carryable>(out Carryable carry) && carry != PlayerStats._player.CarriedItem)
            {
                carryPrompt.enabled = true;
                carryPrompt.text = $"Press E to Carry";
                return;
            }



            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                carryPrompt.enabled = true;
                carryPrompt.text = $"Press E to interact";
                return;

            }



        }
        carryPrompt.enabled = false;
    }

}
