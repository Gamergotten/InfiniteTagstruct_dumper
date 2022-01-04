using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Memory;
using static InfiniteTagstruct_dumper.StructureLayouts;
using System.Xml;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run_stripped run = new Run_stripped();
            run.run_the_program_but_not_on_the_main_class();
        }
    }



    public class Run
    {
        public XmlWriter textWriter;

        Mem m = new Mem();
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

            List<Group_definitions_struct> list_of_stuff_to_be_dumped = new();
            for (int iteration_index = 0; iteration_index < target_number_of_iterations; iteration_index++)
            {
                string temp_filename = @"C:\Users\Connor\Downloads\test\out\dump" + iteration_index + ".xml";
                using (XmlWriter w = XmlWriter.Create(temp_filename, xmlWriterSettings))
                {
                    textWriter = w;
                    textWriter.WriteStartDocument();

                    textWriter.WriteStartElement("root");

                    long offset_from_start = iteration_index * 88;
                    long current_tag_struct_Address = starting_address + offset_from_start;
                    Group_definitions_struct gdshgfjasdf = read_one_group_definition_at_a_time(current_tag_struct_Address);
                    list_of_stuff_to_be_dumped.Add(gdshgfjasdf);

                    textWriter.WriteEndElement();
                    textWriter.WriteEndDocument();
                    textWriter.Close();

                    System.IO.FileInfo fi = new System.IO.FileInfo(temp_filename);
                    // Check if file is there  
                    if (fi.Exists)
                    {
                        // Move file with a new name. Hence renamed.  
                        fi.MoveTo(@"C:\Users\Connor\Downloads\test\out\_" + gdshgfjasdf.tag_definition_name + ".xml");
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
        public Group_definitions_struct read_one_group_definition_at_a_time(long address)
        {
            Group_definitions_struct gds = new Group_definitions_struct
            {
                tag_definition_name = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                int1 = m.ReadInt((address + 8).ToString("X")),
                tag_def_short_name = m.ReadString((address + 12).ToString("X"), "", 8),
                int2 = m.ReadInt((address + 20).ToString("X")),
                exe_pointer1 = m.ReadLong((address + 24).ToString("X")),

                int3 = m.ReadInt((address + 40).ToString("X")),
                tag_thing = m.ReadString((address + 44).ToString("X"), "", 12),

                tag_struct_pointer = read_a_Group_definitions_link_struct(m.ReadLong((address + 32).ToString("X"))), // next
            };
            return gds;
        }
        public Group_definitions_link_struct read_a_Group_definitions_link_struct(long address)
        {
            Group_definitions_link_struct gdls = new Group_definitions_link_struct
            {
                name1 = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                name2 = m.ReadString(m.ReadLong((address + 8).ToString("X")).ToString("X")),

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
            List<Table1_struct> list_to_return_with_everything_else = new List<Table1_struct>();
            long address_for_our_string_bruh = m.ReadLong(address.ToString("X"));
            string take_this_mf_and_pass_it_down_for_gods_sake = m.ReadString(address_for_our_string_bruh.ToString("X"));

            for (int index = 0; index < amount_of_things_to_read; index++)
            {
                list_to_return_with_everything_else.Add(read_the_intermediate_thing(m.ReadLong((address + 32).ToString("X")) + (index * 24), take_this_mf_and_pass_it_down_for_gods_sake));
            }

            Table2_struct big_and_chunky = new Table2_struct
            {
                Name1 = take_this_mf_and_pass_it_down_for_gods_sake,
                Name2 = m.ReadString(m.ReadLong((address + 8).ToString("X")).ToString("X")),

                hash1 = m.ReadInt((address + 16).ToString("X")).ToString("X"),
                hash2 = m.ReadInt((address + 20).ToString("X")).ToString("X"),
                hash3 = m.ReadInt((address + 24).ToString("X")).ToString("X"),
                hash4 = m.ReadInt((address + 28).ToString("X")).ToString("X"),

                int1 = m.ReadInt((address + 40).ToString("X")),
                int2 = m.ReadInt((address + 44).ToString("X")),

                exe_pointer1 = m.ReadLong((address + 48).ToString("X")),

                int3 = m.ReadInt((address + 56).ToString("X")),
                int4 = m.ReadInt((address + 60).ToString("X")),
                int5 = m.ReadInt((address + 64).ToString("X")),
                int6 = m.ReadInt((address + 68).ToString("X")),

                hash5 = m.ReadInt((address + 72).ToString("X")).ToString("X"),
                hash6 = m.ReadInt((address + 76).ToString("X")).ToString("X"),

                int7 = m.ReadInt((address + 80).ToString("X")),
                int8 = m.ReadInt((address + 84).ToString("X")),

                tag_struct_lookup2 = m.ReadLong((address + 88).ToString("X")),

                Name3 = m.ReadString(m.ReadLong((address + 96).ToString("X")).ToString("X")),

                int9 = m.ReadInt((address + 104).ToString("X")),
                int10 = m.ReadInt((address + 108).ToString("X")),

                unknown_pointer1 = m.ReadLong((address + 112).ToString("X")),
                unknown_string4 = m.ReadString((address + 120).ToString("X"), "", 4), // 4bytes

                int11 = m.ReadInt((address + 124).ToString("X")),
                int12 = m.ReadInt((address + 128).ToString("X")),
                int13 = m.ReadInt((address + 132).ToString("X")),

                tag_struct_lookup3 = m.ReadLong((address + 136).ToString("X")),

                STRUCTCOUNT = m.ReadInt((address + 144).ToString("X")),
                int14 = m.ReadInt((address + 148).ToString("X")),
                int15 = m.ReadInt((address + 152).ToString("X")),
                int16 = m.ReadInt((address + 156).ToString("X")),
                int17 = m.ReadInt((address + 160).ToString("X")),
                int18 = m.ReadInt((address + 164).ToString("X")),
                int19 = m.ReadInt((address + 168).ToString("X")),
                int20 = m.ReadInt((address + 172).ToString("X")),
                int21 = m.ReadInt((address + 176).ToString("X")),
                int22 = m.ReadInt((address + 180).ToString("X")),
                int23 = m.ReadInt((address + 184).ToString("X")),
                int24 = m.ReadInt((address + 188).ToString("X")),

                exe_pointer2 = m.ReadLong((address + 192).ToString("X")),

                tag_struct_lookup1 = list_to_return_with_everything_else, // do this later
            };
            return big_and_chunky;
        }
        public Table1_struct read_the_intermediate_thing(long address, string real_name_100)
        {
            int group = m.ReadInt((address + 8).ToString("X"));
            string n_name = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X"));
            Table1_struct t1s = new Table1_struct
            {
                name = n_name,
                struct_type_index = group,
                int2 = m.ReadInt((address + 12).ToString("X")),
                dodgy_struct = read_the_annoying_changy_ones(m.ReadLong((address + 16).ToString("X")), group, address, n_name), // real_name_100
            };
            textWriter.WriteEndElement();
            return t1s;
        }
        public possible_t1_struct_c_instance? read_the_annoying_changy_ones(long address, int grouptype, long remove_when_not_debugging_if_you_want, string name_for_xml_debugg)
        {
            textWriter.WriteStartElement("_" + grouptype.ToString("X"));
            textWriter.WriteAttributeString("v", "_" + name_for_xml_debugg);
            switch (grouptype)
            {
                case 0x2:
                    possible_t1_struct_c_instance ptsct_02 = new possible_t1_struct_c_instance
                    {
                        _02_ = new _02
                        {
                            exe_pointer = m.ReadLong(address.ToString("X"))
                        }
                    };

                    return ptsct_02;
                case 0xA:
                    return putting_this_inside_of_a_method_because_its_used_Too_many_times(address);
                case 0xB:
                    return putting_this_inside_of_a_method_because_its_used_Too_many_times(address);
                case 0xC:
                    return putting_this_inside_of_a_method_because_its_used_Too_many_times(address);
                case 0xD:
                    return putting_this_inside_of_a_method_because_its_used_Too_many_times(address);
                case 0xE:
                    return putting_this_inside_of_a_method_because_its_used_Too_many_times(address);
                case 0xF:
                    return putting_this_inside_of_a_method_because_its_used_Too_many_times(address);
                case 0x29:
                    return new possible_t1_struct_c_instance
                    {
                        _29_ = new _29
                        {
                            //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                        }
                    };
                case 0x2A:
                    return new possible_t1_struct_c_instance
                    {
                        _2A_ = new _2A
                        {
                            //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                        }
                    };
                case 0x2B:
                    return new possible_t1_struct_c_instance
                    {
                        _2B_ = new _2B
                        {
                            //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                        }
                    };
                case 0x2C:
                    return new possible_t1_struct_c_instance
                    {
                        _2C_ = new _2C
                        {
                            //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                        }
                    };
                case 0x2D:
                    return new possible_t1_struct_c_instance
                    {
                        actual_value = address
                    };
                case 0x2E:
                    return new possible_t1_struct_c_instance
                    {
                        _2E_ = new _2E
                        {
                            //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                        }
                    };
                case 0x2F:
                    return new possible_t1_struct_c_instance
                    {
                        actual_value = address
                    };
                case 0x30:
                    return new possible_t1_struct_c_instance
                    {
                        _30_ = new _30
                        {
                            //tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                        }
                    };
                case 0x31:
                    return new possible_t1_struct_c_instance
                    {
                        actual_value = address
                    };
                case 0x34:
                    return new possible_t1_struct_c_instance
                    {
                        actual_value = address
                    };
                case 0x35:
                    return new possible_t1_struct_c_instance
                    {
                        actual_value = address
                    };
                case 0x36:
                    return new possible_t1_struct_c_instance
                    {
                        actual_value = address
                    };
                case 0x37:
                    return new possible_t1_struct_c_instance
                    {
                        actual_value = address
                    };
                case 0x38:
                    return new possible_t1_struct_c_instance
                    {
                        _38_ = new _38
                        {
                            table2_ref = read_the_big_chunky_one(address)
                        }
                    };
                case 0x39:
                    return new possible_t1_struct_c_instance
                    {
                        _39_ = new _39
                        {
                            Name1 = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                            int1 = m.ReadInt((address + 8).ToString("X")),
                            int2 = m.ReadInt((address + 12).ToString("X")),
                            long1 = m.ReadLong((address + 16).ToString("X")),
                            //table2_ref = read_the_big_chunky_one(address) // bruh this in the wrong spot
                        }
                    };
                case 0x40:
                    return new possible_t1_struct_c_instance
                    {
                        _40_ = new _40
                        {
                            tag_struct_pointer = read_a_Group_definitions_link_struct(address)
                        }
                    };
                case 0x41:
                    long child_address = m.ReadLong((address + 136).ToString("X"));
                    return new possible_t1_struct_c_instance
                    {
                        _41_ = new _41
                        {
                            int1 = m.ReadInt((address + 0).ToString("X")),
                            taggroup1 = m.ReadString((address + 4).ToString("X"), "", 4),

                            taggroup2 = m.ReadString((child_address + 0).ToString("X"), "", 4),
                            taggroup3 = m.ReadString((child_address + 4).ToString("X"), "", 4),
                            taggroup4 = m.ReadString((child_address + 8).ToString("X"), "", 4),
                            taggroup5 = m.ReadString((child_address + 12).ToString("X"), "", 4)
                        }
                    };
                case 0x42:
                    return new possible_t1_struct_c_instance
                    {
                        _42_ = new _42
                        {
                            Name1 = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                            int1 = m.ReadInt((address + 8).ToString("X")),
                            int2 = m.ReadInt((address + 12).ToString("X")),
                            int3 = m.ReadInt((address + 16).ToString("X")),
                            int4 = m.ReadInt((address + 20).ToString("X")),
                            long1 = m.ReadLong((address + 24).ToString("X")),
                            long2 = m.ReadLong((address + 32).ToString("X")),
                            long3 = m.ReadLong((address + 40).ToString("X")),
                            long4 = m.ReadLong((address + 48).ToString("X")),
                            long5 = m.ReadLong((address + 56).ToString("X")),
                            long6 = m.ReadLong((address + 64).ToString("X")),
                        }
                    };
                case 0x43:
                    return new possible_t1_struct_c_instance
                    {
                        _43_ = new _43
                        {
                            Name1 = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                            long1 = m.ReadLong((address + 8).ToString("X")),
                            //table2_ref = read_the_big_chunky_one(address+16),
                            long2 = m.ReadLong((address + 24).ToString("X")),
                        }
                    };
                default:
                    if (address != 0)
                    {
                        Console.WriteLine("found an unmapped type: " + grouptype.ToString("X") + "(" + grouptype + ") @" + remove_when_not_debugging_if_you_want.ToString("X"));
                    }
                    return null;
            }


            return null;
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

                string reuse_me_uh = m.ReadString(m.ReadLong((address_WHY_WONT_YOU_WORK + (i * 8)).ToString("X")).ToString("X"));
                childs.Add(reuse_me_uh);

                textWriter.WriteAttributeString("v", "_" + reuse_me_uh);


                textWriter.WriteEndElement();
            }

            possible_t1_struct_c_instance ptsct_0A = new possible_t1_struct_c_instance
            {
                _0B_through_0F_ = new _0B_through_0F
                {
                    name = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                    count = count_of_children,
                    children = childs
                }
            };

            return ptsct_0A;
        }
    }

    public class Run_stripped
    {
        public XmlWriter textWriter;

        Mem m = new Mem();
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

            for (int iteration_index = 0; iteration_index < target_number_of_iterations; iteration_index++)
            {
                string temp_filename = @"C:\Users\Connor\Downloads\test\out\dump" + iteration_index + ".xml";
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
                    string name_thingo = m.ReadString(m.ReadLong(current_tag_struct_Address.ToString("X")).ToString("X"));

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
                        fi.MoveTo(@"C:\Users\Connor\Downloads\test\out\_" + name_thingo + ".xml");
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
                name1 = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                name2 = m.ReadString(m.ReadLong((address + 8).ToString("X")).ToString("X")),

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
            string take_this_mf_and_pass_it_down_for_gods_sake = m.ReadString(address_for_our_string_bruh.ToString("X"));

            for (int index = 0; index < amount_of_things_to_read; index++)
            {
                long address_next_next = m.ReadLong((address + 32).ToString("X")) + (index * 24);

                int group = m.ReadInt((address_next_next + 8).ToString("X"));
                string n_name = m.ReadString(m.ReadLong(address_next_next.ToString("X")).ToString("X"));

                long next_next_next_address = m.ReadLong((address_next_next + 16).ToString("X"));
                //    , group, address_next_next, ); // real_name_100
                //
                textWriter.WriteStartElement("_" + group.ToString("X"));
                textWriter.WriteAttributeString("v", "_" + n_name);
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
                        new possible_t1_struct_c_instance
                        {
                            actual_value = next_next_next_address
                        };
                        break;
                    case 0x35:
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
                                Name1 = m.ReadString(m.ReadLong(next_next_next_address.ToString("X")).ToString("X")),
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
                                Name1 = m.ReadString(m.ReadLong(next_next_next_address.ToString("X")).ToString("X")),
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
                                Name1 = m.ReadString(m.ReadLong(next_next_next_address.ToString("X")).ToString("X")),
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

                string reuse_me_uh = m.ReadString(m.ReadLong((address_WHY_WONT_YOU_WORK + (i * 8)).ToString("X")).ToString("X"));
                childs.Add(reuse_me_uh);

                textWriter.WriteAttributeString("v", "_" + reuse_me_uh);


                textWriter.WriteEndElement();
            }

            possible_t1_struct_c_instance ptsct_0A = new possible_t1_struct_c_instance
            {
                _0B_through_0F_ = new _0B_through_0F
                {
                    name = m.ReadString(m.ReadLong(address.ToString("X")).ToString("X")),
                    count = count_of_children,
                    children = childs
                }
            };

            return ptsct_0A;
        }
    }
}
