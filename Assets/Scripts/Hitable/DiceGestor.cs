using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGestor : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> m_dotsFace1;
    [SerializeField] private List<SpriteRenderer> m_dotsFace2;
    [SerializeField] private List<SpriteRenderer> m_dotsFace3;

    Matrix4x4[] m_faces = new Matrix4x4[]{ 
        new Matrix4x4(new Vector4(0,0,0,0), new Vector4(0,1,0,0), new Vector4(0,0,0,0), new Vector4(0,0,0,0)),
        new Matrix4x4(new Vector4(1,0,0,0), new Vector4(0,0,0,0), new Vector4(0,0,1,0), new Vector4(0,0,0,0)), 
        new Matrix4x4(new Vector4(1,0,0,0), new Vector4(0,1,0,0), new Vector4(0,0,1,0), new Vector4(0,0,0,0)), 
        new Matrix4x4(new Vector4(1,0,1,0), new Vector4(0,0,0,0), new Vector4(1,0,1,0), new Vector4(0,0,0,0)),  
        new Matrix4x4(new Vector4(1,0,1,0), new Vector4(0,1,0,0), new Vector4(1,0,1,0), new Vector4(0,0,0,0)), 
        new Matrix4x4(new Vector4(1,0,1,0), new Vector4(1,0,1,0), new Vector4(1,0,1,0), new Vector4(0,0,0,0))}; 
    // Start is called before the first frame update


    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    public void Draw(int _face1, int _face2, int _face3)
    {
        Debug.Log(_face1 + " " + _face2 + " " + _face3);
        for (int x = 0; x < 3; ++x)
        {
            for (int y = 0; y < 3; ++y)
            {
                m_dotsFace1[x + y * 3].enabled = m_faces[_face1-1][x + y * 4] == 1.0f;
                m_dotsFace2[x + y * 3].enabled = m_faces[_face2-1][x + y * 4] == 1.0f;
                m_dotsFace3[x + y * 3].enabled = m_faces[_face3-1][x + y * 4] == 1.0f;
            }
        }
        
        this.gameObject.SetActive(true);
    }
}
