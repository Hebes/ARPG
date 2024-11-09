Shader "CameraFilterPack/FX_Funk"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
  }
  SubShader
  {
    Tags
    { 
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZClip Off
      ZTest Always
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _MainTex;
      uniform float _TimeX;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR :COLOR;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = in_v.color;
          float2 tmpvar_2;
          tmpvar_2 = in_v.texcoord.xy;
          float2 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5.w = 1;
          tmpvar_5.xyz = in_v.vertex.xyz;
          tmpvar_3 = tmpvar_2;
          tmpvar_4 = tmpvar_1;
          out_v.xlv_TEXCOORD0 = tmpvar_3;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_5));
          out_v.xlv_COLOR = tmpvar_4;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float2 uv_1;
          uv_1 = in_f.xlv_TEXCOORD0;
          float4 col_2;
          float n_3;
          float2 tuv_4;
          tuv_4 = uv_1;
          uv_1 = (uv_1 * 2.5);
          float tmpvar_5;
          tmpvar_5 = (((sin(((1.099609 + (_TimeX * 2.704406)) + (3 * uv_1.x))) + sin(((0.455078 + (_TimeX * 2.124281)) - (4 * uv_1.x)))) + sin(((8.447266 + (_TimeX * 1.902469)) + (2 * uv_1.y)))) + sin(((610.4609 + (_TimeX * 2.439938)) + (5 * uv_1.y))));
          n_3 = tmpvar_5;
          float tmpvar_6;
          tmpvar_6 = ((4 + tmpvar_5) / 4);
          float tmpvar_7;
          tmpvar_7 = frac(abs(tmpvar_6));
          float tmpvar_8;
          if((tmpvar_6>=0))
          {
              tmpvar_8 = tmpvar_7;
          }
          else
          {
              tmpvar_8 = (-tmpvar_7);
          }
          float4 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex, tuv_4);
          col_2 = tmpvar_9;
          n_3 = (tmpvar_8 + (((col_2.x * 0.2) + (col_2.y * 0.4)) + (col_2.z * 0.2)));
          float tmpvar_10;
          tmpvar_10 = frac(abs(n_3));
          float tmpvar_11;
          if((n_3>=0))
          {
              tmpvar_11 = tmpvar_10;
          }
          else
          {
              tmpvar_11 = (-tmpvar_10);
          }
          float tmpvar_12;
          tmpvar_12 = (tmpvar_11 * 6);
          float tmpvar_13;
          tmpvar_13 = (clamp((tmpvar_12 - 4), 0, 1) + clamp((2 - tmpvar_12), 0, 1));
          float tmpvar_14;
          if((tmpvar_12<2))
          {
              tmpvar_14 = clamp(tmpvar_12, 0, 1);
          }
          else
          {
              tmpvar_14 = clamp((4 - tmpvar_12), 0, 1);
          }
          float tmpvar_15;
          if((tmpvar_12<4))
          {
              tmpvar_15 = clamp((tmpvar_12 - 2), 0, 1);
          }
          else
          {
              tmpvar_15 = clamp((6 - tmpvar_12), 0, 1);
          }
          float3 tmpvar_16;
          tmpvar_16.x = tmpvar_13;
          tmpvar_16.y = tmpvar_14;
          tmpvar_16.z = tmpvar_15;
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = float3(tmpvar_16);
          out_f.color = tmpvar_17;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
