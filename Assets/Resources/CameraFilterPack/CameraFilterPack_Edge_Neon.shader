Shader "CameraFilterPack/Edge_Neon"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _TimeX ("Time", Range(0, 1)) = 1
    _Distortion ("_Distortion", Range(0, 1)) = 0.3
    _ScreenResolution ("_ScreenResolution", Vector) = (0,0,0,0)
    _EdgeWeight ("_EdgeWeight", Range(1, 10)) = 1
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
      uniform float _EdgeWeight;
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
          float4 t_2;
          float4 gy_3;
          float4 gx_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          float tmpvar_6;
          tmpvar_6 = (1 / (_EdgeWeight * 100));
          float3 tmpvar_7;
          tmpvar_7.y = 0;
          tmpvar_7.x = (-tmpvar_6);
          tmpvar_7.z = tmpvar_6;
          float2 P_8;
          P_8 = (uv_5 + tmpvar_7.xz);
          gx_4 = tex2D(_MainTex, P_8);
          gy_3 = gx_4;
          float2 P_9;
          P_9 = (uv_5 + tmpvar_7.xy);
          gx_4 = (gx_4 + (2 * tex2D(_MainTex, P_9)));
          float4 tmpvar_10;
          float2 P_11;
          P_11 = (uv_5 + tmpvar_7.xx);
          tmpvar_10 = tex2D(_MainTex, P_11);
          t_2 = tmpvar_10;
          gx_4 = (gx_4 + t_2);
          gy_3 = (gy_3 - t_2);
          float2 P_12;
          P_12 = (uv_5 + tmpvar_7.yz);
          gy_3 = (gy_3 + (2 * tex2D(_MainTex, P_12)));
          float2 P_13;
          P_13 = (uv_5 + tmpvar_7.yx);
          gy_3 = (gy_3 - (2 * tex2D(_MainTex, P_13)));
          float4 tmpvar_14;
          float2 P_15;
          P_15 = (uv_5 + float2(tmpvar_6, tmpvar_6));
          tmpvar_14 = tex2D(_MainTex, P_15);
          t_2 = tmpvar_14;
          gx_4 = (gx_4 - t_2);
          gy_3 = (gy_3 + t_2);
          float2 P_16;
          P_16 = (uv_5 + tmpvar_7.zy);
          gx_4 = (gx_4 - (2 * tex2D(_MainTex, P_16)));
          float4 tmpvar_17;
          float2 P_18;
          P_18 = (uv_5 + tmpvar_7.zx);
          tmpvar_17 = tex2D(_MainTex, P_18);
          t_2 = tmpvar_17;
          gx_4 = (gx_4 - t_2);
          gy_3 = (gy_3 - t_2);
          float4 tmpvar_19;
          float _tmp_dvx_66 = sqrt(((gx_4 * gx_4) + (gy_3 * gy_3)));
          tmpvar_19 = float4(_tmp_dvx_66, _tmp_dvx_66, _tmp_dvx_66, _tmp_dvx_66);
          tmpvar_1 = tmpvar_19;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
