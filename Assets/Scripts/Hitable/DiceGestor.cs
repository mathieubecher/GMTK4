using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DiceGestor : MonoBehaviour
{
    public enum RotationFaceUp
    {
        Rotate0,
        Rotate90,
        Rotate180,
        Rotate270
    }
    
    [FormerlySerializedAs("m_dotsFace1")] [SerializeField] private List<SpriteRenderer> m_dotsFaceLeft;
    [FormerlySerializedAs("m_dotsFace2")] [SerializeField] private List<SpriteRenderer> m_dotsFaceRight;
    [FormerlySerializedAs("m_dotsFace3")] [SerializeField] private List<SpriteRenderer> m_dotsFaceUp;

    Matrix4x4[] m_faces = new Matrix4x4[]{ 
        new Matrix4x4(new Vector4(0,0,0,0), new Vector4(0,1,0,0), new Vector4(0,0,0,0), new Vector4(0,0,0,0)),
        new Matrix4x4(new Vector4(0,1,0,0), new Vector4(0,0,0,0), new Vector4(0,1,0,0), new Vector4(0,0,0,0)), 
        new Matrix4x4(new Vector4(1,0,0,0), new Vector4(0,1,0,0), new Vector4(0,0,1,0), new Vector4(0,0,0,0)), 
        new Matrix4x4(new Vector4(1,0,1,0), new Vector4(0,0,0,0), new Vector4(1,0,1,0), new Vector4(0,0,0,0)),  
        new Matrix4x4(new Vector4(1,0,1,0), new Vector4(0,1,0,0), new Vector4(1,0,1,0), new Vector4(0,0,0,0)), 
        new Matrix4x4(new Vector4(1,0,1,0), new Vector4(1,0,1,0), new Vector4(1,0,1,0), new Vector4(0,0,0,0))}; 
    // Start is called before the first frame update

    private static int[] axisOne = new int[]{4, 2, 3, 5};
    private static int[] axisTwo = new int[]{1, 4, 6, 3};
    private static int[] axisThree = new int[]{1, 2, 6, 5};
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    public void Draw(int _faceUp, RotationFaceUp _rotation)
    {
        int faceUp = _faceUp;
        int[] axis = _faceUp == 1 || _faceUp == 6 ? axisOne : _faceUp == 2 || _faceUp == 5 ? axisTwo : axisThree;
        int numberLeftInAxis = Random.Range(0, 4);
        
        int faceLeft = axis[numberLeftInAxis];
        int faceRight = axis[(numberLeftInAxis + (faceUp > 3 ? -1 : 1)) % 4];
        Debug.Log(_faceUp + " " + faceLeft + " " + faceRight);
        for (int x = 0; x < 3; ++x)
        {
            for (int y = 0; y < 3; ++y)
            {
                m_dotsFaceLeft[x + y * 3].enabled = m_faces[faceLeft-1][x + y * 4] == 1.0f;
                m_dotsFaceRight[x + y * 3].enabled = m_faces[faceRight-1][x + y * 4] == 1.0f;
                m_dotsFaceUp[x + y * 3].enabled = m_faces[faceUp-1][x + y * 4] == 1.0f;
            }
        }
        
        this.gameObject.SetActive(true);
    }
}
