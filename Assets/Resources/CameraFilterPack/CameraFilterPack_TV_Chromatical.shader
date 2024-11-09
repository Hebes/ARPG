Shader "CameraFilterPack/TV_Chromatical"
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
          float3 col_1;
          float blur_2;
          float2 uv_3;
          uv_3 = in_f.xlv_TEXCOORD0;
          float tmpvar_4;
          float2 x_5;
          x_5 = (uv_3 - float2(0.5, 0.5));
          tmpvar_4 = sqrt(dot(x_5, x_5));
          blur_2 = ((1 + sin((_TimeX * 6))) * 0.5);
          blur_2 = (blur_2 * (1 + (sin((_TimeX * 16)) * 0.5)));
          blur_2 = (pow(blur_2, 3) * 0.05);
          blur_2 = (blur_2 * tmpvar_4);
          float2 tmpvar_6;
          tmpvar_6.x = (uv_3.x + blur_2);
          tmpvar_6.y = uv_3.y;
          float4 tmpvar_7;
          tmpvar_7 = tex2D(_MainTex, tmpvar_6);
          col_1.x = tmpvar_7.x;
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, uv_3);
          col_1.y = tmpvar_8.y;
          float2 tmpvar_9;
          tmpvar_9.x = (uv_3.x - blur_2);
          tmpvar_9.y = uv_3.y;
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex, tmpvar_9);
          col_1.z = tmpvar_10.z;
          col_1 = (col_1 - (sin((uv_3.y * 800)) * 0.04));
          col_1 = (col_1 * (1 - (tmpvar_4 * 0.5)));
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(col_1);
          out_f.color = tmpvar_11;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
