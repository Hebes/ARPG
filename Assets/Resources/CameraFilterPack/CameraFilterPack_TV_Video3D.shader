Shader "CameraFilterPack/TV_Video3D"
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
          float height_3;
          float2 p2_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          uv_5 = (floor((uv_5 * 500)) / 500);
          p2_4 = float2(0, 0);
          height_3 = 0;
          int l_2 = 0;
          while((l_2<80))
          {
              float tmpvar_6;
              tmpvar_6 = (uv_5.y - (float(l_2) * 0.002));
              float2 tmpvar_7;
              tmpvar_7.x = uv_5.x;
              tmpvar_7.y = (tmpvar_6 * 2);
              float2 tmpvar_8;
              tmpvar_8 = (tmpvar_7 + float2(0.75, 0.375));
              float tmpvar_9;
              float2 p_10;
              p_10 = (tmpvar_8 * 0.4);
              float4 tmpvar_11;
              tmpvar_11 = tex2D(_MainTex, p_10);
              tmpvar_9 = (tmpvar_11.x * 0.2);
              if(((tmpvar_6 + tmpvar_9)>uv_5.y))
              {
                  p2_4 = tmpvar_8;
                  height_3 = tmpvar_9;
              }
              l_2 = (l_2 + 1);
          }
          float2 P_12;
          P_12 = (p2_4 * 0.4);
          float3 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, P_12).xyz;
          col_1 = tmpvar_13;
          float tmpvar_14;
          float2 p_15;
          p_15 = (p2_4 + float2(0, 0.004));
          p_15 = (p_15 * 0.4);
          float4 tmpvar_16;
          tmpvar_16 = tex2D(_MainTex, p_15);
          tmpvar_14 = (tmpvar_16.x * 0.2);
          float tmpvar_17;
          float2 p_18;
          p_18 = (p2_4 - float2(0, 0.004));
          p_18 = (p_18 * 0.4);
          float4 tmpvar_19;
          tmpvar_19 = tex2D(_MainTex, p_18);
          tmpvar_17 = (tmpvar_19.x * 0.2);
          float tmpvar_20;
          float2 p_21;
          p_21 = (p2_4 + float2(0.004, 0));
          p_21 = (p_21 * 0.4);
          float4 tmpvar_22;
          tmpvar_22 = tex2D(_MainTex, p_21);
          tmpvar_20 = (tmpvar_22.x * 0.2);
          float tmpvar_23;
          float2 p_24;
          p_24 = (p2_4 - float2(0.004, 0));
          p_24 = (p_24 * 0.4);
          float4 tmpvar_25;
          tmpvar_25 = tex2D(_MainTex, p_24);
          tmpvar_23 = (tmpvar_25.x * 0.2);
          float3 tmpvar_26;
          tmpvar_26.x = 0;
          tmpvar_26.y = 0.008;
          tmpvar_26.z = max(0.003, (tmpvar_14 - tmpvar_17));
          float3 tmpvar_27;
          tmpvar_27.y = 0;
          tmpvar_27.x = 0.008;
          tmpvar_27.z = max(0.003, (tmpvar_20 - tmpvar_23));
          col_1 = (col_1 * max(0.2, dot(normalize(((tmpvar_26.yzx * tmpvar_27.zxy) - (tmpvar_26.zxy * tmpvar_27.yzx))), float3(0.7071068, 0, (-0.7071068)))));
          float tmpvar_28;
          float2 p_29;
          p_29 = (p2_4 + float2(0, 0.004));
          p_29 = (p_29 * 0.4);
          float4 tmpvar_30;
          tmpvar_30 = tex2D(_MainTex, p_29);
          tmpvar_28 = (tmpvar_30.x * 0.2);
          float tmpvar_31;
          float2 p_32;
          p_32 = (p2_4 - float2(0, 0.004));
          p_32 = (p_32 * 0.4);
          float4 tmpvar_33;
          tmpvar_33 = tex2D(_MainTex, p_32);
          tmpvar_31 = (tmpvar_33.x * 0.2);
          float tmpvar_34;
          float2 p_35;
          p_35 = (p2_4 + float2(0.004, 0));
          p_35 = (p_35 * 0.4);
          float4 tmpvar_36;
          tmpvar_36 = tex2D(_MainTex, p_35);
          tmpvar_34 = (tmpvar_36.x * 0.2);
          float tmpvar_37;
          float2 p_38;
          p_38 = (p2_4 - float2(0.004, 0));
          p_38 = (p_38 * 0.4);
          float4 tmpvar_39;
          tmpvar_39 = tex2D(_MainTex, p_38);
          tmpvar_37 = (tmpvar_39.x * 0.2);
          float3 tmpvar_40;
          tmpvar_40.x = 0;
          tmpvar_40.y = 0.008;
          tmpvar_40.z = max(0.003, (tmpvar_28 - tmpvar_31));
          float3 tmpvar_41;
          tmpvar_41.y = 0;
          tmpvar_41.x = 0.008;
          tmpvar_41.z = max(0.003, (tmpvar_34 - tmpvar_37));
          col_1 = (col_1 * (1 + pow(max(0, dot(normalize(((tmpvar_40.yzx * tmpvar_41.zxy) - (tmpvar_40.zxy * tmpvar_41.yzx))), float3(0.7071068, 0, (-0.7071068)))), 6)));
          float3 tmpvar_42;
          tmpvar_42.x = ((uv_5.x * 0.8) - 0.4);
          tmpvar_42.y = uv_5.y;
          tmpvar_42.z = height_3;
          float tmpvar_43;
          float tmpvar_44;
          tmpvar_44 = max(0, sqrt(dot(tmpvar_42, tmpvar_42)));
          tmpvar_43 = (tmpvar_44 * tmpvar_44);
          float3 tmpvar_45;
          tmpvar_45 = ((col_1 * (1 - tmpvar_43)) + (float3(0.8, 0.8, 0.8) * tmpvar_43));
          col_1 = tmpvar_45;
          float4 tmpvar_46;
          tmpvar_46.w = 1;
          tmpvar_46.xyz = float3(tmpvar_45);
          out_f.color = tmpvar_46;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
