using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Memory;
using static InfiniteTagstruct_dumper.StructureLayouts;
using System.Xml;
using System.Text.RegularExpressions;
namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool selected = false;
            while (!selected)
            {
                Console.WriteLine("\"dump\" to dump the xmls");
                Console.WriteLine("\"parse\" to parse the xmls");

                string choice = Console.ReadLine();
                if (choice == "dump")
                {
                    Run_stripped run = new Run_stripped();
                    run.run_the_program_but_not_on_the_main_class();
                }
                else if (choice == "parse")
                {
                    run_parse runp = new();
                    runp.parse_the_mfing_xmls();
                }
            }
        }
    }

    public class Run_stripped
    {
        public XmlWriter textWriter;

        Mem m = new Mem();
        public static string ReverseString(string myStr)
        {
            char[] myArr = myStr.ToCharArray();
            Array.Reverse(myArr);
            return new string(myArr);
        }

        public void run_the_program_but_not_on_the_main_class()
        {
            bool is_program_hooked_to_infinite = false;
            while (!is_program_hooked_to_infinite)
            {
                is_program_hooked_to_infinite = return_attempted_hook_result();
            }


            Console.WriteLine("Enter the starting Address");
            string read_address_from_user = Console.ReadLine();

            Console.WriteLine("Enter the count to dump");
            string count_to_read_from_user = Console.ReadLine();

            long starting_address = Convert.ToInt64(read_address_from_user, 16); // convert from hex 
            int target_number_of_iterations = int.Parse(count_to_read_from_user);

            Console.WriteLine("Enter the folder directory to dump to");
            string dump_dir = Console.ReadLine();

            for (int iteration_index = 0; iteration_index < target_number_of_iterations; iteration_index++)
            {
                string temp_filename = dump_dir + @"\dump" + iteration_index + ".xml";
                using (XmlWriter w = XmlWriter.Create(temp_filename, xmlWriterSettings))
                {
                    textWriter = w;
                    textWriter.WriteStartDocument();

                    textWriter.WriteStartElement("root");

                    long offset_from_start = iteration_index * 88;
                    long current_tag_struct_Address = starting_address + offset_from_start;
                    //
                    long gdshgfjasdf = (current_tag_struct_Address);
                    // read_one_group_definition_at_a_time
                    //string name_thingo = m.ReadString(m.ReadLong(current_tag_struct_Address.ToString("X")).ToString("X")); //
                    string group_name_thingo = m.ReadString((current_tag_struct_Address + 12).ToString("X"), "", 4); //


                    read_a_Group_definitions_link_struct(m.ReadLong((current_tag_struct_Address + 32).ToString("X"))); // next

                    //


                    textWriter.WriteEndElement();
                    textWriter.WriteEndDocument();
                    textWriter.Close();

                    System.IO.FileInfo fi = new System.IO.FileInfo(temp_filename);
                    // Check if file is there  
                    if (fi.Exists)
                    {
                        // Move file with a new name. Hence renamed.  
                        string s33 = ReverseString(group_name_thingo);
                        if (!s33.Contains("*"))
                        {
                            fi.MoveTo(dump_dir + @"\" + s33 + ".xml");
                        }
                        else
                        {
                            string s331 = s33.Replace("*", "_");
                            fi.MoveTo(dump_dir + @"\" + s331 + ".xml");
                            Console.WriteLine(s33 + " replaced with " + s331);
                        }
                    }

                }
            }

            Console.WriteLine("done loaded");
            Console.ReadLine();
            Console.WriteLine("press to exit");
            Console.ReadLine();
        }
        XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            Indent = true,
            IndentChars = "\t",
        };

        public bool return_attempted_hook_result()
        {
            Console.WriteLine("press a button to attempt to hook!");
            Console.ReadLine();
            bool did_our_process_hook = m.OpenProcess("HaloInfinite.exe");
            return did_our_process_hook;
        }
        public Group_definitions_link_struct read_a_Group_definitions_link_struct(long address)
        {
            Group_definitions_link_struct gdls = new Group_definitions_link_struct
            {
                name1 = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X"), "", 300),
                name2 = m.ReadString(m.ReadLong((address + 8).ToString("X")).ToString("X"), "", 300),

                int1 = m.ReadInt((address + 16).ToString("X")),
                int2 = m.ReadInt((address + 20).ToString("X")), // potential count

                Table2_struct_pointer2 = m.ReadLong((address + 24).ToString("X")),
                Table2_struct = read_the_big_chunky_one(m.ReadLong((address + 24).ToString("X"))), // next

            };

            return gdls;
        }
        public Table2_struct read_the_big_chunky_one(long address)
        {

            int amount_of_things_to_read = m.ReadInt((address + 144).ToString("X"));

            long address_for_our_string_bruh = m.ReadLong(address.ToString("X"));
            string take_this_mf_and_pass_it_down_for_gods_sake = m.ReadString(address_for_our_string_bruh.ToString("X"), "", 300);

            for (int index = 0; index < amount_of_things_to_read; index++)
            {
                long address_next_next = m.ReadLong((address + 32).ToString("X")) + (index * 24);

                int group = m.ReadInt((address_next_next + 8).ToString("X"));
                string n_name = m.ReadString(m.ReadLong(address_next_next.ToString("X")).ToString("X"), "", 300);

                long next_next_next_address = m.ReadLong((address_next_next + 16).ToString("X"));
                //    , group, address_next_next, ); // real_name_100
                //
                textWriter.WriteStartElement("_" + group.ToString("X"));
                textWriter.WriteAttributeString("v", n_name);
                switch (group)
                {
                    case 0x2:
                        possible_t1_struct_c_instance ptsct_02 = new possible_t1_struct_c_instance
                        {
                            _02_ = new _02
                            {
                                exe_pointer = m.ReadLong(next_next_next_address.ToString("X"))
                            }
                        };
                        break;
                    case 0xA:
                        putting_this_inside_of_a_method_because_its_used_Too_many_times(next_next_next_address);
                        break;
                    case 0xB:
                        putting_this_inside_of_a_method_because_its_used_Too_many_times(next_next_next_address);
                        break;
                    case 0xC:
                        putting_this_inside_of_a_method_because_its_used_Too_many_times(next_next_next_address);
                        break;
                    case 0xD:
                        putting_this_inside_of_a_method_because_its_used_Too_many_times(next_next_next_address);
                        break;
                    case 0xE:
                        putting_this_inside_of_a_method_because_its_used_Too_many_times(next_next_next_address);
                        break;
                    case 0xF:
                        putting_this_inside_of_a_method_because_its_used_Too_many_times(next_next_next_address);
                        break;
                    case 0x29:
                        new possible_t1_struct_c_instance
                        {
                            _29_ = new _29
                            {
                                //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                            }
                        };
                        break;
                    case 0x2A:
                        new possible_t1_struct_c_instance
                        {
                            _2A_ = new _2A
                            {
                                //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                            }
                        };
                        break;
                    case 0x2B:
                        new possible_t1_struct_c_instance
                        {
                            _2B_ = new _2B
                            {
                                //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                            }
                        };
                        break;
                    case 0x2C:
                        new possible_t1_struct_c_instance
                        {
                            _2C_ = new _2C
                            {
                                //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                            }
                        };
                        break;
                    case 0x2D:
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x2E:
                        new possible_t1_struct_c_instance
                        {
                            _2E_ = new _2E
                            {
                                //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                            }
                        };
                        break;
                    case 0x2F:
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x30:
                        new possible_t1_struct_c_instance
                        {
                            _30_ = new _30
                            {
                                //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                            }
                        };
                        break;
                    case 0x31:
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x34:
                        textWriter.WriteAttributeString("length", next_next_next_address.ToString());
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x35:
                        textWriter.WriteAttributeString("length", next_next_next_address.ToString());
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x36:
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x37:
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x38:
                        new possible_t1_struct_c_instance
                        {
                            _38_ = new _38
                            {
                                table2_ref = read_the_big_chunky_one(next_next_next_address)
                            }
                        };
                        break;
                    case 0x39:
                        new possible_t1_struct_c_instance
                        {
                            _39_ = new _39
                            {
                                Name1 = m.ReadString(m.ReadLong(next_next_next_address.ToString("X")).ToString("X"), "", 300),
                                int1 = m.ReadInt((next_next_next_address + 8).ToString("X")),
                                int2 = m.ReadInt((next_next_next_address + 12).ToString("X")),
                                long1 = m.ReadLong((next_next_next_address + 16).ToString("X")),
                                //table2_ref = read_the_big_chunky_one(address) // bruh this in the wrong spot
                            }
                        };
                        break;
                    case 0x40:
                        new possible_t1_struct_c_instance
                        {
                            _40_ = new _40
                            {
                                tag_struct_pointer = read_a_Group_definitions_link_struct(next_next_next_address)
                            }
                        };
                        break;
                    case 0x41:
                        long child_address = m.ReadLong((next_next_next_address + 136).ToString("X"));
                        new possible_t1_struct_c_instance
                        {
                            _41_ = new _41
                            {
                                int1 = m.ReadInt((next_next_next_address + 0).ToString("X")),
                                taggroup1 = m.ReadString((next_next_next_address + 4).ToString("X"), "", 4),

                                taggroup2 = m.ReadString((child_address + 0).ToString("X"), "", 4),
                                taggroup3 = m.ReadString((child_address + 4).ToString("X"), "", 4),
                                taggroup4 = m.ReadString((child_address + 8).ToString("X"), "", 4),
                                taggroup5 = m.ReadString((child_address + 12).ToString("X"), "", 4)
                            }
                        };
                        break;
                    case 0x42:
                        new possible_t1_struct_c_instance
                        {
                            _42_ = new _42
                            {
                                Name1 = m.ReadString(m.ReadLong(next_next_next_address.ToString("X")).ToString("X"), "", 300),
                                int1 = m.ReadInt((next_next_next_address + 8).ToString("X")),
                                int2 = m.ReadInt((next_next_next_address + 12).ToString("X")),
                                int3 = m.ReadInt((next_next_next_address + 16).ToString("X")),
                                int4 = m.ReadInt((next_next_next_address + 20).ToString("X")),
                                long1 = m.ReadLong((next_next_next_address + 24).ToString("X")),
                                long2 = m.ReadLong((next_next_next_address + 32).ToString("X")),
                                long3 = m.ReadLong((next_next_next_address + 40).ToString("X")),
                                long4 = m.ReadLong((next_next_next_address + 48).ToString("X")),
                                long5 = m.ReadLong((next_next_next_address + 56).ToString("X")),
                                long6 = m.ReadLong((next_next_next_address + 64).ToString("X")),
                            }
                        };
                        break;
                    case 0x43:
                        new possible_t1_struct_c_instance
                        {
                            _43_ = new _43
                            {
                                Name1 = m.ReadString(m.ReadLong(next_next_next_address.ToString("X")).ToString("X"), "", 300),
                                long1 = m.ReadLong((next_next_next_address + 8).ToString("X")),
                                //table2_ref = read_the_big_chunky_one(address+16),
                                long2 = m.ReadLong((next_next_next_address + 24).ToString("X")),
                            }
                        };
                        break;
                }

                //
                textWriter.WriteEndElement();


            }
            return new Table2_struct { };
        }


        public possible_t1_struct_c_instance putting_this_inside_of_a_method_because_its_used_Too_many_times(long address)
        {


            int count_of_children = m.ReadInt((address + 8).ToString("X"));
            long children_address = m.ReadLong((address + 16).ToString("X"));
            List<string> childs = new();

            for (int i = 0; i < count_of_children; i++)
            {
                textWriter.WriteStartElement("Flag");

                long address_WHY_WONT_YOU_WORK = m.ReadLong((address + 16).ToString("X"));

                string reuse_me_uh = m.ReadString(m.ReadLong((address_WHY_WONT_YOU_WORK + (i * 8)).ToString("X")).ToString("X"), "", 300);
                childs.Add(reuse_me_uh);

                textWriter.WriteAttributeString("v", reuse_me_uh);


                textWriter.WriteEndElement();
            }

            possible_t1_struct_c_instance ptsct_0A = new possible_t1_struct_c_instance
            {
                _0B_through_0F_ = new _0B_through_0F
                {
                    name = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X"), "", 300),
                    count = count_of_children,
                    children = childs
                }
            };

            return ptsct_0A;
        }
    }

    public class run_parse
    {
        public string parsed_thing = "";
        public long evalutated_index_PREVENT_DICTIONARYERROR = 0;
        public void parse_the_mfing_xmls()
        {
            Console.WriteLine("select directory to dump");
            string folder = Console.ReadLine();

            string[] files = Directory.GetFiles(folder);
            foreach (string file in files)
            {
        parsed_thing = "";

        FileInfo fi = new FileInfo(file);
                if (fi.Extension == ".xml")
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    if (name.Contains("_"))
                    {
                        name = name.Replace("_", "*");
                    }

                    parsed_thing += "{ \"" + name + "\", new()\r\n{\r\n";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(file);
                    XmlNode xn = xd.SelectSingleNode("root");
                    parse_one_mf_at_time(xn);
                    parsed_thing += "}},\r\n\r\n";

                    File.WriteAllText(file + ".txt", parsed_thing);
                }
            }


        }
        public void parse_one_mf_at_time(XmlNode xn)
        {
            XmlNodeList xnl = xn.ChildNodes;
            long current_offset = 0;
            foreach (XmlNode xntwo in xnl)
            {

                current_offset += the_switch_statement(xntwo, current_offset);
            }
        }

        public long the_switch_statement(XmlNode xn, long offset)
        {
            string s = "{ 0x";
            string b1 = ", new C{ T=\"";
            string b2 = "\", N = \"";
            string e = "\" }},\r\n";
            switch (xn.Name)
            {
                case "_0":
                    parsed_thing += s + offset.ToString("X") + b1 + "String" + b2 + xn.Attributes.GetNamedItem("v").InnerText + e;
                    return group_lengths_dict[xn.Name];
                case "_1":
                    parsed_thing += s + offset.ToString("X") + b1 + "String" + b2 + xn.Attributes.GetNamedItem("v").InnerText + e;
                    return group_lengths_dict[xn.Name];
                case "_2":
                    parsed_thing += s + offset.ToString("X") + b1 + "mmr3Hash" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_3":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_4":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_5":
                    parsed_thing += s + offset.ToString("X") + b1 + "2Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_6":
                    parsed_thing += s + offset.ToString("X") + b1 + "4Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_7":
                    parsed_thing += s + offset.ToString("X") + b1 + "Pointer" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_8":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_9":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_A":

                    parsed_thing += "{ 0x" + offset.ToString("X") + ", new EnumGroup { A=1, N=\"" + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + "\", STR = new Dictionary<int, string>()\r\n{\r\n";
                    for (int iu = 0; iu < xn.ChildNodes.Count; iu++)
                    {
                        parsed_thing += "{" + iu + ", \"" + xn.ChildNodes[iu].Attributes.GetNamedItem("v").InnerText + "\" },\r\n";
                    }
                    parsed_thing += "}}},\r\n";

                    return group_lengths_dict[xn.Name];
                case "_B":
                    parsed_thing += "{ 0x" + offset.ToString("X") + ", new EnumGroup { A=2, N=\"" + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + "\", STR = new Dictionary<int, string>()\r\n{\r\n";
                    for (int iu = 0; iu < xn.ChildNodes.Count; iu++)
                    {
                        parsed_thing += "{" + iu + ", \"" + xn.ChildNodes[iu].Attributes.GetNamedItem("v").InnerText + "\" },\r\n";
                    }
                    parsed_thing += "}}},\r\n";

                    return group_lengths_dict[xn.Name];
                case "_C": // 
                    parsed_thing += "{ 0x" + offset.ToString("X") + ", new EnumGroup { A=4, N=\"" + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + "\", STR = new Dictionary<int, string>()\r\n{\r\n";
                    for (int iu = 0; iu < xn.ChildNodes.Count; iu++)
                    {
                        parsed_thing += "{" + iu + ", \"" + xn.ChildNodes[iu].Attributes.GetNamedItem("v").InnerText + "\" },\r\n";
                    }
                    parsed_thing += "}}},\r\n";

                    return group_lengths_dict[xn.Name];
                case "_D":

                    parsed_thing += "{ 0x" + offset.ToString("X") + ", new FlagGroup { A=4, N=\"" + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + "\", STR = new Dictionary<int, string>()\r\n{\r\n";
                    for (int iu = 0; iu < xn.ChildNodes.Count; iu++)
                    {
                        parsed_thing += "{" + iu + ", \"" + xn.ChildNodes[iu].Attributes.GetNamedItem("v").InnerText + "\" },\r\n";
                    }
                    parsed_thing += "}}},\r\n";
                    return group_lengths_dict[xn.Name];
                case "_E":
                    parsed_thing += "{ 0x" + offset.ToString("X") + ", new FlagGroup { A=2, N=\"" + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + "\", STR = new Dictionary<int, string>()\r\n{\r\n";
                    for (int iu = 0; iu < xn.ChildNodes.Count; iu++)
                    {
                        parsed_thing += "{" + iu + ", \"" + xn.ChildNodes[iu].Attributes.GetNamedItem("v").InnerText + "\" },\r\n";
                    }
                    parsed_thing += "}}},\r\n";
                    return group_lengths_dict[xn.Name];
                case "_F":
                    parsed_thing += "{ 0x" + offset.ToString("X") + ", new FlagGroup { A=1, N=\"" + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + "\", STR = new Dictionary<int, string>()\r\n{\r\n";
                    for (int iu = 0; iu < xn.ChildNodes.Count; iu++)
                    {
                        parsed_thing += "{" + iu + ", \"" + xn.ChildNodes[iu].Attributes.GetNamedItem("v").InnerText + "\" },\r\n";
                    }
                    parsed_thing += "}}},\r\n";
                    return group_lengths_dict[xn.Name];
                case "_10":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_11":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_12":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_13":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_14":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_15":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_16":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_17":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".X" + e;
                    parsed_thing += s + (offset + 4).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".Y" + e;
                    parsed_thing += s + (offset + 8).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".Z" + e;
                    return group_lengths_dict[xn.Name];
                case "_18":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_19":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".X" + e;
                    parsed_thing += s + (offset + 4).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".Y" + e;
                    parsed_thing += s + (offset + 8).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".Z" + e;
                    return group_lengths_dict[xn.Name];
                case "_1A":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_1B":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_1C":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".X" + e;
                    parsed_thing += s + (offset + 4).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".Y" + e;
                    parsed_thing += s + (offset + 8).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".Z" + e;
                    return group_lengths_dict[xn.Name];
                case "_1D":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_1E":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_1F":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_20":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_21":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_22":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_23":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_24":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".min" + e;
                    parsed_thing += s + (offset + 4).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".max" + e;
                    return group_lengths_dict[xn.Name];
                case "_25":
                    parsed_thing += s + offset.ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".min" + e;
                    parsed_thing += s + (offset + 4).ToString("X") + b1 + "Float" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + ".max" + e;
                    return group_lengths_dict[xn.Name];
                case "_26":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_27":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_28":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_29":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_2A":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_2B":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_2C":
                    parsed_thing += s + offset.ToString("X") + b1 + "Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_2D":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_2E":
                    parsed_thing += s + offset.ToString("X") + b1 + "2Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_2F":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_30":
                    parsed_thing += s + offset.ToString("X") + b1 + "4Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_31":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_32":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_33":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_34": // field pad
                    int length = int.Parse(xn.Attributes.GetNamedItem("length").InnerText);
                    if (length == 1)
                    {
                        parsed_thing += s + offset.ToString("X") + b1 + "Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    }
                    else if (length == 2)
                    {
                        parsed_thing += s + offset.ToString("X") + b1 + "2Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    }
                    else if (length == 4)
                    {
                        parsed_thing += s + offset.ToString("X") + b1 + "4Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    }
                    else
                    {
                        parsed_thing += s + offset.ToString("X") + b1 + "Comment" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    }
                    return length;
                case "_35":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return int.Parse(xn.Attributes.GetNamedItem("length").InnerText);
                case "_36":
                    parsed_thing += s + offset.ToString("X") + evalutated_index_PREVENT_DICTIONARYERROR + b1 + "Comment" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    evalutated_index_PREVENT_DICTIONARYERROR++;
                    return group_lengths_dict[xn.Name];
                case "_37":
                    parsed_thing += s + offset.ToString("X") + evalutated_index_PREVENT_DICTIONARYERROR + b1 + "Comment" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    evalutated_index_PREVENT_DICTIONARYERROR++;
                    return group_lengths_dict[xn.Name];
                case "_38":
                    parsed_thing += s + offset.ToString("X") + evalutated_index_PREVENT_DICTIONARYERROR + b1 + "Comment" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    evalutated_index_PREVENT_DICTIONARYERROR++;
                    XmlNodeList xnl1 = xn.ChildNodes;
                    long current_offset1 = offset;
                    foreach (XmlNode xntwo2 in xnl1)
                    {
                        current_offset1 += the_switch_statement(xntwo2, current_offset1);
                    }
                    return current_offset1 - offset;
                case "_39":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_3A":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_3B":
                    return group_lengths_dict[xn.Name];
                case "_3C":
                    parsed_thing += s + offset.ToString("X") + b1 + "Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_3D":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_3E":
                    parsed_thing += s + offset.ToString("X") + b1 + "4Byte" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_3F":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_40":
                    if (xn.ChildNodes.Count > 0)
                    {
                        parsed_thing += s + offset.ToString("X") + b1 + "Tagblock" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + "\", B = new Dictionary<long, C> \r\n{\r\n";


                        XmlNodeList xnl2 = xn.ChildNodes;
                        long current_offset2 = 0;
                        foreach (XmlNode xntwo2 in xnl2)
                        {
                            current_offset2 += the_switch_statement(xntwo2, current_offset2);
                        }
                        parsed_thing += "}, S=" + current_offset2 + "}},\r\n";
                    }
                    else
                    {
                        parsed_thing += s + offset.ToString("X") + b1 + "Tagblock" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    }
                    return group_lengths_dict[xn.Name];
                case "_41":
                    parsed_thing += s + offset.ToString("X") + b1 + "TagRef" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_42":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_43":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_44":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];
                case "_45":
                    parsed_thing += s + offset.ToString("X") + b1 + "Unmapped" + b2 + Regex.Replace(xn.Attributes.GetNamedItem("v").InnerText, @"\t|\n|\r", "") + e;
                    return group_lengths_dict[xn.Name];


            }
            return group_lengths_dict[xn.Name];
        }
    }
}


