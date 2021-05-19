using UnityEngine;

public class CharacterParticle : MonoBehaviour
{

  private GameObject LocalHitParticle;
  private GameObject LocalCoinParticle;
  public Character character;
  public GameObject CoinParticle;
  public GameObject HitParticle;
  private void Start()
  {
    LocalHitParticle = Instantiate(HitParticle, Character.Position, Quaternion.identity);
    LocalHitParticle.transform.localScale = new Vector3(5, 5, 5);
    LocalCoinParticle = Instantiate(CoinParticle, Character.Position, Quaternion.identity);
  }
  private void Update()
  {
    LocalHitParticle.transform.position = Character.Position;
    LocalCoinParticle.transform.position = new Vector3(Character.Position.x, Character.Position.y + 5, Character.Position.z);
  }
  public void PlayHitParticle()
  {
    LocalHitParticle.GetComponent<ParticleSystem>().Play();
  }
  public void PlayCoinParticle()
  {
    LocalCoinParticle.GetComponent<ParticleSystem>().Play();
  }
}