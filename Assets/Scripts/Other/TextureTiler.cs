using UnityEngine;
 
 public class TextureTiler : MonoBehaviour {
     [SerializeField] private float tileX = 1;
     [SerializeField] private float tileY = 1;
     Mesh mesh;
     private Material materal;

     void Start() {
         materal = GetComponent<Renderer>().material;
         mesh = GetComponent<MeshFilter>().mesh;
         
     }
 
     void Update() {
         materal.mainTextureScale = new Vector2((mesh.bounds.size.x * transform.localScale.x)/100*tileX, (mesh.bounds.size.y * transform.localScale.y)/100*tileY);
     }
 }