using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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

    private int m_valueFaceUp = 1;
    private int m_valueFaceRight = 3;
    private int m_valueFaceLeft = 2;
    
    private static int[] axisOne = new int[]{4, 2, 3, 5};
    private static int[] axisTwo = new int[]{1, 4, 6, 3};
    private static int[] axisThree = new int[]{1, 2, 6, 5};

    public int Roll(bool _preserveFaceRight)
    {
        if (_preserveFaceRight)
        {
            int randomX = Random.Range(1,4);
            int randomY = Random.Range(1,4);
            m_faces[m_valueFaceLeft - 1][randomX + randomY * 4] = 1f;
            
            int[] axis = m_valueFaceRight == 1 || m_valueFaceRight == 6 ? axisOne : m_valueFaceRight == 2 || m_valueFaceRight == 5 ? axisTwo : axisThree;
            int numberLeftInAxis = Random.Range(0, 4);
            m_valueFaceUp = axis[numberLeftInAxis];
            Debug.Log("Preserve Face Right : " + m_valueFaceUp + " " + numberLeftInAxis);
            m_valueFaceLeft = axis[(numberLeftInAxis + (m_valueFaceRight > 3 ? -1 : 1) + 4) % 4];
        }
        else
        {
            int randomX = Random.Range(1,4);
            int randomY = Random.Range(1,4);
            m_faces[m_valueFaceLeft - 1][randomX + randomY * 4] = 1f;
            
            int[] axis = m_valueFaceLeft == 1 || m_valueFaceLeft == 6 ? axisOne : m_valueFaceLeft == 2 || m_valueFaceLeft == 5 ? axisTwo : axisThree;
            int numberLeftInAxis = Random.Range(0, 4);
            m_valueFaceUp = axis[numberLeftInAxis];
            Debug.Log("Preserve Face Left : " + m_valueFaceUp + " " + numberLeftInAxis);
            m_valueFaceRight = axis[(numberLeftInAxis + (m_valueFaceLeft > 3 ? 1 : -1) + 4) % 4];
        }

        Draw();
        return m_valueFaceUp;
    }

    public void Draw(int _valueFaceUp)
    {
        m_valueFaceUp = _valueFaceUp;
        int[] axis = m_valueFaceUp == 1 || m_valueFaceUp == 6 ? axisOne : m_valueFaceUp == 2 || m_valueFaceUp == 5 ? axisTwo : axisThree;
        int numberLeftInAxis = Random.Range(0, 4);
        m_valueFaceLeft = axis[numberLeftInAxis];
        // Debug.Log("Preserve Face Right : " + m_valueFaceUp + " " + numberLeftInAxis);
        m_valueFaceRight = axis[(numberLeftInAxis + (m_valueFaceLeft > 3 ? -1 : 1) + 4) % 4];
        Draw();
    }
    
    private void Draw()
    {
        for (int x = 0; x < 3; ++x)
        {
            for (int y = 0; y < 3; ++y)
            {
                m_dotsFaceLeft[x + y * 3].enabled = m_faces[m_valueFaceLeft-1][x + y * 4] == 1.0f;
                m_dotsFaceRight[x + y * 3].enabled = m_faces[m_valueFaceRight-1][x + y * 4] == 1.0f;
                m_dotsFaceUp[x + y * 3].enabled = m_faces[m_valueFaceUp-1][x + y * 4] == 1.0f;
            }
        }
    }

}
