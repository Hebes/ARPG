Shader "CameraFilterPack/Special_Bubble"
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
          float3 r_1;
          float2 v_2;
          float a_3;
          float2 p_4;
          float2 tmpvar_5;
          tmpvar_5 = ((in_f.xlv_TEXCOORD0 * 2) - float2(1, 1));
          p_4 = tmpvar_5;
          p_4 = (p_4 * 2);
          float tmpvar_6;
          tmpvar_6 = (p_4.x - _Value);
          float tmpvar_7;
          tmpvar_7 = (p_4.y - _Value2);
          float tmpvar_8;
          float tmpvar_9;
          tmpvar_9 = (_TimeX * 3);
          tmpvar_8 = (tmpvar_6 + (sin((tmpvar_9 * 3)) * 0.15));
          float tmpvar_10;
          tmpvar_10 = (tmpvar_7 + (cos(tmpvar_9) * 0.8));
          float tmpvar_11;
          float tmpvar_12;
          tmpvar_12 = (_TimeX * 1.9);
          tmpvar_11 = (tmpvar_6 + (sin((tmpvar_12 * 3)) * 0.2));
          float tmpvar_13;
          tmpvar_13 = (tmpvar_7 + (cos(tmpvar_12) * 0.8));
          a_3 = ((rsqrt(((tmpvar_8 * tmpvar_8) + (tmpvar_10 * tmpvar_10))) * sin(tmpvar_9)) + (rsqrt(((tmpvar_11 * tmpvar_11) + (tmpvar_13 * tmpvar_13))) * sin(tmpvar_12)));
          float tmpvar_14;
          float tmpvar_15;
          tmpvar_15 = (_TimeX * 0.6);
          tmpvar_14 = (tmpvar_6 + (sin((tmpvar_15 * 3)) * 0.17));
          float tmpvar_16;
          tmpvar_16 = (tmpvar_7 + (cos(tmpvar_15) * 0.4));
          a_3 = (a_3 + (rsqrt(((tmpvar_14 * tmpvar_14) + (tmpvar_16 * tmpvar_16))) * sin(tmpvar_15)));
          float tmpvar_17;
          float tmpvar_18;
          tmpvar_18 = (_TimeX * 1.3);
          tmpvar_17 = (tmpvar_6 + (sin((tmpvar_18 * 3)) * 0.14));
          float tmpvar_19;
          tmpvar_19 = (tmpvar_7 + (cos(tmpvar_18) * 0.6));
          a_3 = (a_3 + (rsqrt(((tmpvar_17 * tmpvar_17) + (tmpvar_19 * tmpvar_19))) * sin(tmpvar_18)));
          float tmpvar_20;
          float tmpvar_21;
          tmpvar_21 = (_TimeX * 1.8);
          tmpvar_20 = (tmpvar_6 + (sin((tmpvar_21 * 3)) * 0.14));
          float tmpvar_22;
          tmpvar_22 = (tmpvar_7 + (cos(tmpvar_21) * 0.5));
          a_3 = (a_3 + (rsqrt(((tmpvar_20 * tmpvar_20) + (tmpvar_22 * tmpvar_22))) * sin(tmpvar_21)));
          float3 tmpvar_23;
          tmpvar_23.x = a_3;
          tmpvar_23.y = (a_3 - (tmpvar_7 * 32));
          tmpvar_23.z = (a_3 - (tmpvar_7 * 50));
          float2 tmpvar_24;
          tmpvar_24 = (in_f.xlv_TEXCOORD0 * 0.8);
          v_2 = tmpvar_24;
          float2 P_25;
          P_25 = (v_2 - float2(((tmpvar_23 / 32).x * _Value3).xy));
          float3 tmpvar_26;
          tmpvar_26 = tex2D(_MainTex, P_25).xyz;
          r_1 = tmpvar_26;
          float4 tmpvar_27;
          tmpvar_27.w = 1;
          tmpvar_27.xyz = float3(r_1);
          out_f.color = tmpvar_27;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
