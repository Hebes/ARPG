Shader "PS4/airDistort/screen/Twirt Effect"
{
  Properties
  {
    _Color ("Tint", Color) = (1,1,1,0)
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "Transparent+99"
      "RenderType" = "Transparent+10"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Stencil
      { 
        Ref 0
        ReadMask 0
        WriteMask 0
        Pass Keep
        Fail Keep
        ZFail Keep
        PassFront Keep
        FailFront Keep
        ZFailFront Keep
        PassBack Keep
        FailBack Keep
        ZFailBack Keep
      } 
      Fog
      { 
        Mode  Off
      } 
      // m_ProgramMask = 0
      
    } // end phase
    Pass // ind: 2, name: 
    {
      Tags
      { 
        "QUEUE" = "Transparent+99"
        "RenderType" = "Transparent+10"
      }
      ZClip Off
      ZTest Always
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform sampler2D _GrabTexture;
      uniform float2 _Center;
      uniform float2 _Radius;
      uniform float4x4 _RotationMatrix;
      uniform float4 _Color;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float2 tmpvar_1;
          tmpvar_1 = in_v.texcoord.xy;
          float2 tmpvar_2;
          float4 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5.w = 1;
          tmpvar_5.xyz = in_v.vertex.xyz;
          tmpvar_4 = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_5));
          tmpvar_2 = tmpvar_1;
          tmpvar_3.xy = ((tmpvar_4.xy + tmpvar_4.w) * 0.5);
          tmpvar_3.zw = tmpvar_4.zw;
          out_v.vertex = tmpvar_4;
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          out_v.xlv_TEXCOORD1 = tmpvar_3;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 col_1;
          float2 offset_2;
          float2 tmpvar_3;
          tmpvar_3 = (in_f.xlv_TEXCOORD0 - _Center);
          float4 tmpvar_4;
          tmpvar_4.zw = float2(0, 0);
          tmpvar_4.xy = float2(tmpvar_3);
          float2 tmpvar_5;
          tmpvar_5 = (tmpvar_3 / _Radius);
          float _tmp_dvx_166 = min(1, sqrt(dot(tmpvar_5, tmpvar_5)));
          offset_2 = (lerp(mul(_RotationMatrix, tmpvar_4).xy, tmpvar_3, float2(_tmp_dvx_166, _tmp_dvx_166)) + _Center);
          offset_2 = (offset_2 * (in_f.xlv_TEXCOORD1.xy / in_f.xlv_TEXCOORD0));
          float4 tmpvar_6;
          tmpvar_6 = tex2D(_GrabTexture, offset_2);
          col_1 = tmpvar_6;
          col_1.xyz = (col_1.xyz + (_Color.xyz * _Color.w));
          out_f.color = col_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
