Shader "CameraFilterPack/Distortion_ShockWave"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
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
      uniform float _Value;
      uniform float _Value2;
      uniform float _Value3;
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
          float4 Color_1;
          float Change_2;
          float ScaleDiff_3;
          float2 texCoord_4;
          float tmpvar_5;
          tmpvar_5 = (_TimeX * _Value3);
          float tmpvar_6;
          tmpvar_6 = (tmpvar_5 * ((tmpvar_5 - floor(tmpvar_5)) / tmpvar_5));
          float2 tmpvar_7;
          tmpvar_7.x = _Value;
          tmpvar_7.y = _Value2;
          texCoord_4 = in_f.xlv_TEXCOORD0;
          float tmpvar_8;
          float2 tmpvar_9;
          tmpvar_9 = (texCoord_4 - tmpvar_7);
          tmpvar_8 = sqrt(dot(tmpvar_9, tmpvar_9));
          ScaleDiff_3 = 1;
          Change_2 = 0;
          if(((tmpvar_8<=(tmpvar_6 + 0.1)) && (tmpvar_8>=(tmpvar_6 - 0.1))))
          {
              float tmpvar_10;
              tmpvar_10 = (tmpvar_8 - tmpvar_6);
              ScaleDiff_3 = (1 - pow(abs((tmpvar_10 * 10)), 0.8));
              Change_2 = 1;
              texCoord_4 = (texCoord_4 + ((normalize((texCoord_4 - tmpvar_7)) * (tmpvar_10 * ScaleDiff_3)) / ((tmpvar_6 * tmpvar_8) * 40)));
          }
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, texCoord_4);
          Color_1 = tmpvar_11;
          Color_1 = (Color_1 + (((Color_1 * ScaleDiff_3) / ((tmpvar_6 * tmpvar_8) * 40)) * Change_2));
          out_f.color = Color_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
