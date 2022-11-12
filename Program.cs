using System;
using System.Xml.Serialization;
using Newtonsoft.Json;


/*C:\Users\Kliker\Desktop\my.json*/

namespace RedactFile
{
    public class RedactFileExtensions
    {
        static Dictionary<int, string> toDict(JsonSruct person)
        {
            Dictionary<int, string> text = new Dictionary<int, string>();

            text.Add(0, person.firstName);
            text.Add(1, person.lastName);
            text.Add(2, person.gender);
            text.Add(3, person.age.ToString());
            text.Add(4, person.address.streetAddress);
            text.Add(5, person.address.city);
            text.Add(6, person.address.state);
            text.Add(7, person.phoneNumbers.type);
            text.Add(8, person.phoneNumbers.number);

            return text;
        }

        static JsonSruct toClass(Dictionary<int, string> text)
        {
            JsonSruct result = new JsonSruct();

            result.firstName = text[0];
            result.lastName = text[1];
            result.gender = text[2];
            result.age = Convert.ToInt32(text[3]);
            result.address.streetAddress = text[4];
            result.address.city = text[5];
            result.address.state = text[6];
            result.phoneNumbers.type = text[7];
            result.phoneNumbers.number = text[8];
            

            return result;
        }


        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите путь до файла: ");
                string path = Console.ReadLine();

                /*Здесь хранится разширение файла*/

                string ext = Path.GetExtension(path);

                if (ext == ".txt")
                {
                    Console.Clear();
                    string[] text = File.ReadAllText(path).Split();
                    JsonSruct result = new JsonSruct();

                    result.firstName = text[0];
                    result.lastName = text[1];
                    result.gender = text[2];
                    result.age = Convert.ToInt32(text[3]);
                    result.address.streetAddress = text[4];
                    result.address.city = text[5];
                    result.address.state = text[6];
                    result.phoneNumbers.type = text[7];
                    result.phoneNumbers.number = text[8];

                    var d1 = ReadLine(toDict(result));
                }

                else if (ext == ".json")
                {
                    Console.Clear();

                    JsonSruct con;

                    using (StreamReader read = new StreamReader(path))
                    {
                        string a = read.ReadToEnd();
                        con = JsonConvert.DeserializeObject<JsonSruct>(a);
                    }

                    
                    Dictionary<int, string> text = new Dictionary<int, string>();

                    text = toDict(con);

                    var clDict = toClass(ReadLine(text));

                }

                else if (ext == ".xml")
                {
                    Console.Clear();
                    XmlSerializer xml = new XmlSerializer(typeof(JsonSruct));
                    JsonSruct js = new JsonSruct();
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        js = (JsonSruct)xml.Deserialize(fs);
                    }
                    var d1 = ReadLine(toDict(js));

                }
                

            }
        }
        static Dictionary<int, string> ReadLine(Dictionary<int, string> text)
        {
            int goPos = 0;


            int verPos = 0;
            int maxVer = text.Count - 1;

            int goMax = text[verPos].Length;
            


            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (verPos <= 0)
                    {
                        verPos = maxVer;
                    }
                    else
                    {
                        verPos--;
                    }

                }
                else if (key.Key == ConsoleKey.DownArrow)
                {

                    if (verPos >= maxVer)
                    {
                        verPos = 0;
                    }
                    else
                    {
                        verPos++;
                    }

                }

                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (goPos >= goMax)
                    {
                        goPos = 0;
                    }
                    else
                    {
                        goPos++;
                    }
                }

                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (goPos <= 0)
                    {
                        goPos = goMax;
                    }
                    else
                    {
                        goPos--;
                    }
                }
                /*Удаление элемента из строки*/
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if(text[verPos].Length != 0)
                    {
                        if (goPos != 0)
                        {
                            text[verPos] = text[verPos].Remove(goPos, 1);
                            goPos = goPos - 1;
                        }

                        else
                        {
                            text[verPos] = text[verPos].Remove(goPos, 1);
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }

                else if (key.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    saveText(text);
                    break;

                }

                Console.Clear();

                foreach (var i in text.Values)
                {
                    Console.WriteLine(i);
                }

                Console.SetCursorPosition(goPos, verPos);
                goMax = text[verPos].Length - 1;
            }

                return text;


        }

        public static void saveText(Dictionary<int, string> ser)
        {
            Console.WriteLine("Введите директорию в, которую сохраните файл:");
            string path = Console.ReadLine();
            string ext = Path.GetExtension(path);
            JsonSruct cl = toClass(ser);

            switch (ext)
            {
                case ".txt":

                    File.Delete(path);
                    
                    
                    

                    foreach (var i in ser.Keys)
                    {
                        if (File.Exists(path))
                        {
                            File.AppendAllText(path, ser[i] + "\n");
                        }
                        else
                        {
                            File.WriteAllText(path, ser[i] + "\n");
                        }

                    }
                    break;

                case ".json":
                    string json = JsonConvert.SerializeObject(cl);
                    File.WriteAllText(path, json);
                    break;

                case ".xml":
                    
                    JsonSruct js = new JsonSruct();
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(JsonSruct));
                        xml.Serialize(fs, cl);
                    }
                    break;
            }

            Console.Clear();

        }
    }
}
