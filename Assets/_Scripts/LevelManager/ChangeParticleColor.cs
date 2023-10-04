using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParticleColor : MonoBehaviour
{
    [SerializeField] private Gradient explorationGradient, battleGradient;
    [SerializeField] Color32 exploration, battle;
    [SerializeField] Camera mainCamera;

    private ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;

    private void Start()
    {
        colorOverLifetimeModule = GetComponent<ParticleSystem>().colorOverLifetime;
        SetExplorationParticles();
    }
    public void SetExplorationParticles()
    {
        colorOverLifetimeModule.color = explorationGradient;
        mainCamera.backgroundColor = exploration;
    }

    public void SetBattleParticles()
    {

        colorOverLifetimeModule.color = battleGradient;
        mainCamera.backgroundColor = battle;
    }
}
