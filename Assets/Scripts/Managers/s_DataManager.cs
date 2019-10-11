using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Managers
{
    public class s_DataManager : s_Singleton<s_DataManager>
    {

        void Start()
        {

        }

        void Update()
        {

        }

        [MenuItem("Tools/Write file")]
        static void WriteString()
        {
            string path = "Assets/Resources/KeyframeData/test.txt";

            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("Test");
            writer.Close();

            //Re-import the file to update the reference in the editor
            AssetDatabase.ImportAsset(path);
            TextAsset asset = Resources.Load("KeyframeData/test") as TextAsset;

            //Print the text from the file
            Debug.Log(asset.text);
        }

        [MenuItem("Tools/Read file")]
        static void ReadString()
        {
            string path = "Assets/Resources/KeyframeData/test.txt";

            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);
            Debug.Log(reader.ReadToEnd());
            reader.Close();
        }

        public void WriteSWATFile(string fileName, List<s_RecordManager.TransformRecording> transformsList)
        {
            // Set path of file output
            string path = "Assets/Resources/KeyframeData/" + fileName + ".txt";

            // Start streamwriter for file
            StreamWriter writer = new StreamWriter(path, true);

            // Output keyframe information
            int i,j;
            for (i = 0; i < transformsList.Count; i++)
            {
                // get current object lists
                List<Vector3> objPositions = transformsList[i].positionsList;
                List<Quaternion> objRotations = transformsList[i].rotationsList;
                // Header for current object
                writer.WriteLine("Object: " + i + "\tTotal Keyframes: " + objPositions.Count);
                // Loop output
                for (j = 0; j < transformsList[i].positionsList.Count; j++)
                {
                    writer.WriteLine(j + "\tPosition: " + objPositions[j].x + "\t" + objPositions[j].y + "\t" + objPositions[j].z
                                     + "\t\tRotation: " + objRotations[j].x + "\t" + objRotations[j].y + "\t" + objRotations[j].z + "\t" + objRotations[j].w);
                }
                // New line seperating entires
                writer.Write("\n");
            }

            // Ouput file generatoin information
            writer.WriteLine("File created on: " + System.DateTime.Now + "\n");
            writer.WriteLine("This 'SWAT' (Some Witty Acronym Textfile) was created for the purposes of the author's Keyframe Animation Tool");
            writer.WriteLine("Information on the Animation Tool can be found on their Github at: https://github.com/Jagman926/AdvAnimation_2019");

            // Close file output
            writer.Close();

            //Re-import the file to update the reference in the editor
            AssetDatabase.ImportAsset(path);
        }
    }
}
