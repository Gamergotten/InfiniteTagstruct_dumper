using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InfiniteTagstruct_dumper
{
    public class StructureLayouts
    {
        public struct Group_definitions_struct // 88 bytes
        {
            public string tag_definition_name;
            public int int1;
            public string tag_def_short_name;
            public int int2;

            public long exe_pointer1;
            public Group_definitions_link_struct tag_struct_pointer;

            public int int3;

            public string tag_thing;
        }
        public struct Group_definitions_link_struct // 40 bytes
        {
            public string name1;
            public string name2;

            public int int1;
            public int int2;

            public Table2_struct Table2_struct; // Table2_struct
            public long Table2_struct_pointer2; // Table2_struct
        }
        public struct Table2_struct // 200 bytes
        {
            public string Name1;
            public string Name2;

            public string hash1;
            public string hash2;
            public string hash3;
            public string hash4;

            public List<Table1_struct> tag_struct_lookup1;

            public int int1; // 384
            public int int2; // 0

            public long exe_pointer1;

            public int int3; // 17
            public int int4; // 0
            public int int5; // 384
            public int int6; // 0

            public string hash5;
            public string hash6;

            public int int7; // 1
            public int int8; // 0

            public long tag_struct_lookup2;

            public string Name3;

            public int int9; // 384
            public int int10; // 0

            public long unknown_pointer1;
            public string unknown_string4;

            public int int11; // 1
            public int int12; // 0
            public int int13; //12387

            public long tag_struct_lookup3;

            public int STRUCTCOUNT; // num of child elements
            public int int14; // 1
            public int int15; // 2664
            public int int16; // 3159044
            public int int17; // 164626464
            public int int18; // 6
            public int int19; // 3159044
            public int int20; // 164626464
            public int int21; // 6
            public int int22; // 0
            public int int23; // 0
            public int int24; // 0

            public long exe_pointer2; // doesn't seem to ever point anywhere
        }



        public struct Table1_struct // 24 bytes
        {
            public string name;
            public int struct_type_index;
            public int int2;
            public possible_t1_struct_c_instance? dodgy_struct; // alternates based on "struct_type_index"
            // primarily a pointer, can also be an int
        }




        // 00
        // 01
        // 02 -- STRING ID
        // 03
        // 04
        // 05
        // 06
        // 07
        // 08
        // 09
        // 0A -- ENUM ??
        // 0B -- ENUM ??
        // 0C
        // 0D -- FLAGS 32
        // 0E
        // 0F -- FLAGS ??
        // 10
        // 11
        // 12
        // 13
        // 14
        // 15
        // 16
        // 17
        // 18
        // 19
        // 1A
        // 1B
        // 1C
        // 1D
        // 1E
        // 1F
        // 20
        // 21
        // 22
        // 23
        // 24
        // 25
        // 26
        // 27
        // 28
        // 29
        // 2A
        // 2B
        // 2C
        // 2D
        // 2E
        // 2F
        // 30
        // 31
        // 32
        // 33
        // 34 -- GENERATED PAD
        // 35
        // 36
        // 37 -- COMMENT
        // 38
        // 39
        // 3A
        // 3B -- END STRUCT
        // 3C
        // 3D
        // 3E
        // 3F
        // 40 -- TAGBLOCK
        // 41 -- TAGREF
        // 42
        // 43




        public struct possible_t1_struct_c_instance
        {
            public long actual_value;
            public _02 _02_;
            public _0B_through_0F _0B_through_0F_; // flags and enums
            public _29 _29_;
            public _2A _2A_;
            public _2B _2B_;
            public _2C _2C_;
            public _2D _2D_;
            public _2E _2E_;
            public _2F _2F_;
            public _30 _30_;
            public _31 _31_;
            public _34 _34_; // generated pad
            public _35 _35_; // another pad?
            public _36 _36_;
            public _37 _37_;
            public _38 _38_;
            public _39 _39_;
            public _40 _40_;
            public _41 _41_;
            public _42 _42_;
            public _43 _43_;
        } // bruh howtf do you store these as a single variable

        public struct _02 // unknown
        {
            public long exe_pointer; // mostly invalid
        }
        public struct _0B_through_0F // flags and enums
        {
            public string name;
            public long count;
            public List<string> children;
        }
        public struct _29
        {
            public Group_definitions_link_struct tag_struct_pointer;
        }
        public struct _2A
        {
            public Group_definitions_link_struct tag_struct_pointer;
        }
        public struct _2B
        {
            public Group_definitions_link_struct tag_struct_pointer;
        }
        public struct _2C
        {
            public Group_definitions_link_struct tag_struct_pointer;
        }
        public struct _2D
        {
            // nothing
        }
        public struct _2E
        {
            public Group_definitions_link_struct tag_struct_pointer;
        }
        public struct _2F
        {
            // pointer to somewhere
        }
        public struct _30
        {
            public Group_definitions_link_struct tag_struct_pointer;
        }
        public struct _31
        {
            // pointer to who knows wheree
        }
        public struct _34
        {
            //public long generated_pad_length;
            // so thers actually nothing in this struct
        }
        public struct _35
        {
            //public long generated_pad_length;
            // so thers actually nothing in this struct
        }
        public struct _36
        {
            // potentially nothing, this points to render stuff
        }
        public struct _37
        {
            // nothing notable aiside from the 4 byte after count
        }
        public struct _38
        {
            public Table2_struct table2_ref;
        }
        public struct _39
        {
            public string Name1;
            public int int1;
            public int int2;
            public long long1;
            public Table2_struct table2_ref;
        }
        public struct _40
        {
            public Group_definitions_link_struct tag_struct_pointer;
        }
        public struct _41
        {
            public int int1;
            public string taggroup1;
            // pointer to
            public string taggroup2;
            public string taggroup3;
            public string taggroup4;
            public string taggroup5;
        }
        public struct _42 // length 72 bytes
        {
            public string Name1;
            public int int1;
            public int int2;
            public int int3;
            public int int4;

            public long long1;
            public long long2;
            public long long3;
            public long long4;  
            public long long5;
            public long long6;  
        }
        public struct _43 // length 72 bytes
        {
            public string Name1;
            public long long1;
            public Table2_struct table2_ref;
            public long long2;

        }
    }
}
