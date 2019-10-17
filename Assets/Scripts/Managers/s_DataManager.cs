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
            int i, j;
            for (i = 0; i < transformsList.Count; i++)
            {
                // get current object lists
                List<Vector3> objPositions = transformsList[i].positionsList;
                List<Quaternion> objRotations = transformsList[i].rotationsList;
                // Header for current object
                writer.WriteLine("Object|" + i + "|Total Keyframes|" + objPositions.Count);
                // Loop output
                for (j = 0; j < transformsList[i].positionsList.Count; j++)
                {
                    writer.WriteLine(j + "|Position|" + objPositions[j].x + "|" + objPositions[j].y + "|" + objPositions[j].z
                                     + "|Rotation|" + objRotations[j].x + "|" + objRotations[j].y + "|" + objRotations[j].z + "|" + objRotations[j].w);
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

        public List<s_RecordManager.TransformRecording> ReadSWATFile(string fileName)
        {
            // Get path of file name
            string path = "Assets/Resources/KeyframeData/" + fileName + ".txt";
            // Checks if file exists
            if (!File.Exists(path))
            {
                // If not, send error message and return null
                Debug.LogErrorFormat("File: " + path + " does not exist");
                return null;
            }
            // If found, parse information into data structure
            List<s_RecordManager.TransformRecording> trList = new List<s_RecordManager.TransformRecording>();

            //Start stream reader and get header information
            StreamReader reader = new StreamReader(path);

            // Temp string
            string buffer;
            string[] splitBuffer;
            int currentObject = 0;
            s_RecordManager.TransformRecording tempFrameData;
            
            // Read information until end of file
            while (!reader.EndOfStream)
            {
                // Parse Information
                buffer = reader.ReadLine();
                splitBuffer = buffer.Split('|');
                // Check first piece of information on split
                if(splitBuffer[0] == null)
                    Debug.Log("End of Object Frames");
                else if(splitBuffer[0] == "Object")
                {
                    currentObject = int.Parse(splitBuffer[1]);
                    Debug.Log("Reading Object: " + currentObject);
                    // Init temp holder for frame data
                    tempFrameData = new s_RecordManager.TransformRecording();
                    tempFrameData.positionsList = new List<Vector3>();
                    tempFrameData.rotationsList = new List<Quaternion>();
                    trList.Add(tempFrameData);
                }
                else if(splitBuffer.Length == 10)
                {
                    // Record information for current object frame
                    Vector3 tempPositionData = new Vector3(float.Parse(splitBuffer[2]), float.Parse(splitBuffer[3]), float.Parse(splitBuffer[4]));
                    Quaternion tempRotationData = new Quaternion(float.Parse(splitBuffer[6]), float.Parse(splitBuffer[7]), float.Parse(splitBuffer[8]) , float.Parse(splitBuffer[9]));
                    // Add position and rotation data to current object lists
                    trList[currentObject].positionsList.Add(tempPositionData);
                    trList[currentObject].rotationsList.Add(tempRotationData);
                }
                else if(splitBuffer[0] == "")
                {
                    Debug.Log("New Line");
                }
                else
                {
                    Debug.Log("!!None of the above!!");
                    for(int i = 0; i < splitBuffer.Length; i++)
                    {
                        Debug.Log(splitBuffer[i]);
                    }
                }
            }

            // Close reader and return recorded information
            reader.Close();
            return trList;
        }
    }
}
