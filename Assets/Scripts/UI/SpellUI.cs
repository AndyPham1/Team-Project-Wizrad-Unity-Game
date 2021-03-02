using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public List<Spell> spells;
    public List<Spell> activeSpells;
    public GameObject spellMenuObject;

    private bool triggered = false;
    private bool isOver = false;
    int currentActiveSpellIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // just in case if the dev forgot to add the lists
        if (spells != null)
        {
            foreach (Spell spell in spells)
            {
                if (spell.spell != null)
                {
                    spell.spell.SetActive(false);
                }
            }
        }

        if (activeSpells != null)
        {
            foreach (Spell spell in activeSpells)
            {
                if (spell.spell != null)
                {
                    spell.spell.SetActive(true);
                }
            }
        }

    }

    private void Update()
    {
        if (spellMenuObject.activeSelf)
        {
            Time.timeScale = 0;
        }
    }

    public void OnDisable()
    {
        Time.timeScale = 1;
    }

    void OnEnable()
    {
        Time.timeScale = 0;
        foreach (Spell spell in spells)
        {
            foreach (Spell activeSpell in activeSpells)
            {
                if (spell.spell == activeSpell.spell)
                {
                    spell.spellButton.interactable = false;
                }
            }

        }
    }

    public void changeSpell(int activeSpellIndex)
    {
        currentActiveSpellIndex = activeSpellIndex;
    }

    public void updateSpell(int newSpellIndex)
    {
        // Disable the current active spell
        if (activeSpells[currentActiveSpellIndex].spell != null)
        {
            activeSpells[currentActiveSpellIndex].spell.SetActive(false);
            activeSpells[currentActiveSpellIndex].spellButton.interactable = true;
        }

        // update the active spell set up
        activeSpells[currentActiveSpellIndex].name = spells[newSpellIndex].name;
        activeSpells[currentActiveSpellIndex].sceneImage.sprite = spells[newSpellIndex].sceneImage.sprite;
        //activeSpells[currentActiveSpellIndex].textHolder.text = spells[newSpellIndex].textHolder.text;
        activeSpells[currentActiveSpellIndex].spell = spells[newSpellIndex].spell;
        spells[newSpellIndex].spellButton.interactable = false;
        activeSpells[currentActiveSpellIndex].spellButton = spells[newSpellIndex].spellButton;

        // Activate the new current active spell
        activeSpells[currentActiveSpellIndex].spell.SetActive(true);
    }

}

[System.Serializable]
public class Spell
{
    public string name;
    public Image sceneImage;
    public int correspondingIndex;
    //public Text textHolder;
    public GameObject spell;
    public Button spellButton;
}
