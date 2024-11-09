Shader "CameraFilterPack/FX_Plasma"
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
          float4 origine_1;
          float4 t2_2;
          float4 t_3;
          float2 uv_4;
          float tmpvar_5;
          tmpvar_5 = (1000 + (sin((_TimeX * 0.11)) * 20));
          float tmpvar_6;
          tmpvar_6 = (800 + (sin((_TimeX * 0.15)) * 22));
          uv_4 = in_f.xlv_TEXCOORD0;
          float2 tmpvar_7;
          tmpvar_7.x = (sin((uv_4.x + (tmpvar_5 * 0.005))) * cos((tmpvar_5 * 0.01)));
          tmpvar_7.y = (cos((uv_4.y + (tmpvar_5 * 0.001))) * cos((tmpvar_5 * 0.02)));
          float4 tmpvar_8;
          float2 P_9;
          P_9 = (tmpvar_7 * 5);
          tmpvar_8 = tex2D(_MainTex, P_9);
          t_3 = tmpvar_8;
          float2 tmpvar_10;
          tmpvar_10.x = sin((uv_4.x + (tmpvar_5 * 0.001)));
          tmpvar_10.y = cos((uv_4.y + (tmpvar_5 * 0.005)));
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex, tmpvar_10);
          t2_2 = tmpvar_11;
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, uv_4);
          origine_1 = tmpvar_12;
          float4 tmpvar_13;
          tmpvar_13.xw = float2(0, 1);
          tmpvar_13.y = (((((t_3.x * sin((tmpvar_5 * (sin((uv_4.y * 0.5)) + (0.01 * sin(((uv_4.x * 5) + tmpvar_6))))))) * sin((((((tmpvar_5 * 0.1) * t2_2.x) * (uv_4.x - sin((tmpvar_6 * 0.05)))) * sin(((uv_4.y - sin((tmpvar_5 * 0.035))) * 5))) + sin((0.1 * tmpvar_6))))) * 0.5) + ((t2_2.x * abs(sin(((tmpvar_5 * (uv_4.x - 0.5)) * sin((uv_4.y + 0.5)))))) * 0.5)) + ((t_3.x * sin((((tmpvar_5 * (uv_4.x - sin((tmpvar_5 * 0.1)))) * sin((uv_4.y - sin((tmpvar_5 * 0.1))))) * 0.2))) * 0.1));
          tmpvar_13.z = (((((t_3.x * sin((tmpvar_6 * (sin((uv_4.y * 0.25)) + (0.01 * sin(((uv_4.x * 3) + tmpvar_6))))))) * abs(sin((((((tmpvar_5 * 0.09) * t2_2.x) * (uv_4.x - sin((tmpvar_6 * 0.04)))) * sin(((uv_4.y - sin((tmpvar_5 * 0.035))) * 5))) + sin((0.1 * tmpvar_6)))))) * 0.5) + ((t2_2.x * abs(sin(((tmpvar_5 * (uv_4.x - 0.5)) * sin((uv_4.y + 0.5)))))) * 0.5)) + ((t_3.x * abs(sin((((tmpvar_5 * (uv_4.x - sin((tmpvar_5 * 0.1)))) * sin((uv_4.y - sin((tmpvar_5 * 0.1))))) * 0.2)))) * 0.1));
          float4 tmpvar_14;
          tmpvar_14 = (tmpvar_13 + origine_1);
          out_f.color = tmpvar_14;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
