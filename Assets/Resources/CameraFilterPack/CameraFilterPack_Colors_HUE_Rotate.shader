Shader "CameraFilterPack/Colors_HUE_Rotate"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _Speed ("_Speed", Range(0, 20)) = 10
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
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _MainTex;
      uniform float _TimeX;
      uniform float _Speed;
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
          float4 tmpvar_1;
          float4 truepixel_2;
          float4 pixel_3;
          float4x4 hueRotation_4;
          float tmpvar_5;
          tmpvar_5 = (_TimeX * _Speed);
          hueRotation_4 = ((float4x4(0.299, 0.299, 0.299, 0, 0.587, 0.587, 0.587, 0, 0.114, 0.114, 0.114, 0, 0, 0, 0, 1) + (float4x4(0.701, (-0.299), (-0.3), 0, (-0.587), 0.413, (-0.588), 0, (-0.114), (-0.114), 0.886, 0, 0, 0, 0, 0) * cos(tmpvar_5))) + (float4x4(0.168, (-0.328), 1.25, 0, 0.33, 0.035, (-1.05), 0, (-0.497), 0.292, (-0.203), 0, 0, 0, 0, 0) * sin(tmpvar_5)));
          pixel_3 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          float4 tmpvar_6;
          tmpvar_6.yzw = float3(0, 0, 0);
          tmpvar_6.x = (pixel_3.x * conv_mxt4x4_0(hueRotation_4).x);
          truepixel_2 = tmpvar_6;
          float4 tmpvar_7;
          tmpvar_7.yzw = truepixel_2.yzw;
          tmpvar_7.x = (truepixel_2.x + (pixel_3.y * conv_mxt4x4_1(hueRotation_4).x));
          float4 tmpvar_8;
          tmpvar_8.yzw = tmpvar_7.yzw;
          tmpvar_8.x = (tmpvar_7.x + (pixel_3.z * conv_mxt4x4_2(hueRotation_4).x));
          float4 tmpvar_9;
          tmpvar_9.yzw = tmpvar_8.yzw;
          tmpvar_9.x = (tmpvar_8.x + (pixel_3.w * conv_mxt4x4_3(hueRotation_4).x));
          float4 tmpvar_10;
          tmpvar_10.xzw = tmpvar_9.xzw;
          tmpvar_10.y = (truepixel_2.y + (pixel_3.x * conv_mxt4x4_0(hueRotation_4).y));
          float4 tmpvar_11;
          tmpvar_11.xzw = tmpvar_10.xzw;
          tmpvar_11.y = (tmpvar_10.y + (pixel_3.y * conv_mxt4x4_1(hueRotation_4).y));
          float4 tmpvar_12;
          tmpvar_12.xzw = tmpvar_11.xzw;
          tmpvar_12.y = (tmpvar_11.y + (pixel_3.z * conv_mxt4x4_2(hueRotation_4).y));
          float4 tmpvar_13;
          tmpvar_13.xzw = tmpvar_12.xzw;
          tmpvar_13.y = (tmpvar_12.y + (pixel_3.w * conv_mxt4x4_3(hueRotation_4).y));
          float4 tmpvar_14;
          tmpvar_14.xyw = tmpvar_13.xyw;
          tmpvar_14.z = (truepixel_2.z + (pixel_3.x * conv_mxt4x4_0(hueRotation_4).z));
          float4 tmpvar_15;
          tmpvar_15.xyw = tmpvar_14.xyw;
          tmpvar_15.z = (tmpvar_14.z + (pixel_3.y * conv_mxt4x4_1(hueRotation_4).z));
          float4 tmpvar_16;
          tmpvar_16.xyw = tmpvar_15.xyw;
          tmpvar_16.z = (tmpvar_15.z + (pixel_3.z * conv_mxt4x4_2(hueRotation_4).z));
          float4 tmpvar_17;
          tmpvar_17.xyw = tmpvar_16.xyw;
          tmpvar_17.z = (tmpvar_16.z + (pixel_3.w * conv_mxt4x4_3(hueRotation_4).z));
          float4 tmpvar_18;
          tmpvar_18.xyz = tmpvar_17.xyz;
          tmpvar_18.w = (truepixel_2.w + (pixel_3.x * conv_mxt4x4_0(hueRotation_4).w));
          float4 tmpvar_19;
          tmpvar_19.xyz = tmpvar_18.xyz;
          tmpvar_19.w = (tmpvar_18.w + (pixel_3.y * conv_mxt4x4_1(hueRotation_4).w));
          float4 tmpvar_20;
          tmpvar_20.xyz = tmpvar_19.xyz;
          tmpvar_20.w = (tmpvar_19.w + (pixel_3.z * conv_mxt4x4_2(hueRotation_4).w));
          float4 tmpvar_21;
          tmpvar_21.xyz = tmpvar_20.xyz;
          tmpvar_21.w = (tmpvar_20.w + (pixel_3.w * conv_mxt4x4_3(hueRotation_4).w));
          truepixel_2 = tmpvar_21;
          tmpvar_1 = tmpvar_21;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
