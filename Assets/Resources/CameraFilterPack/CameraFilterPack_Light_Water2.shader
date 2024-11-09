Shader "CameraFilterPack/Light_Water2"
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
      uniform float _Value4;
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
          float alpha_1;
          float2 c2_2;
          float2 c1_3;
          float2 p_4;
          p_4 = in_f.xlv_TEXCOORD0;
          c1_3 = p_4;
          c2_2 = p_4;
          float2 coord_5;
          coord_5 = p_4;
          float theta_7;
          float col_8;
          float time_9;
          time_9 = (_TimeX * 1.3);
          col_8 = 0;
          theta_7 = 0;
          int i_6 = 0;
          while((i_6<8))
          {
              float2 adjc_10;
              theta_7 = (0.8975979 * float(i_6));
              adjc_10.x = (coord_5.x + (((cos(theta_7) * time_9) * _Value) + (time_9 * _Value2)));
              adjc_10.y = (coord_5.y - (((sin(theta_7) * time_9) * _Value) - (time_9 * _Value3)));
              col_8 = (col_8 + (cos((((adjc_10.x * cos(theta_7)) - (adjc_10.y * sin(theta_7))) * 6)) * _Value4));
              i_6 = (i_6 + 1);
          }
          float tmpvar_11;
          tmpvar_11 = cos(col_8);
          c2_2.x = (p_4.x + 8.53);
          float2 coord_12;
          coord_12 = c2_2;
          float theta_14;
          float col_15;
          float time_16;
          time_16 = (_TimeX * 1.3);
          col_15 = 0;
          theta_14 = 0;
          int i_13 = 0;
          while((i_13<8))
          {
              float2 adjc_17;
              theta_14 = (0.8975979 * float(i_13));
              adjc_17.x = (coord_12.x + (((cos(theta_14) * time_16) * _Value) + (time_16 * _Value2)));
              adjc_17.y = (coord_12.y - (((sin(theta_14) * time_16) * _Value) - (time_16 * _Value3)));
              col_15 = (col_15 + (cos((((adjc_17.x * cos(theta_14)) - (adjc_17.y * sin(theta_14))) * 6)) * _Value4));
              i_13 = (i_13 + 1);
          }
          float tmpvar_18;
          tmpvar_18 = ((0.5 * (tmpvar_11 - cos(col_15))) / 60);
          c2_2.x = p_4.x;
          c2_2.y = (p_4.y + 8.53);
          float2 coord_19;
          coord_19 = c2_2;
          float theta_21;
          float col_22;
          float time_23;
          time_23 = (_TimeX * 1.3);
          col_22 = 0;
          theta_21 = 0;
          int i_20 = 0;
          while((i_20<8))
          {
              float2 adjc_24;
              theta_21 = (0.8975979 * float(i_20));
              adjc_24.x = (coord_19.x + (((cos(theta_21) * time_23) * _Value) + (time_23 * _Value2)));
              adjc_24.y = (coord_19.y - (((sin(theta_21) * time_23) * _Value) - (time_23 * _Value3)));
              col_22 = (col_22 + (cos((((adjc_24.x * cos(theta_21)) - (adjc_24.y * sin(theta_21))) * 6)) * _Value4));
              i_20 = (i_20 + 1);
          }
          float tmpvar_25;
          tmpvar_25 = ((0.5 * (tmpvar_11 - cos(col_22))) / 60);
          c1_3.x = (p_4.x + (tmpvar_18 * 2));
          c1_3.y = (p_4.y + (tmpvar_25 * 2));
          float tmpvar_26;
          tmpvar_26 = (1 + ((tmpvar_18 * tmpvar_25) * 700));
          alpha_1 = tmpvar_26;
          float tmpvar_27;
          tmpvar_27 = (tmpvar_18 - 0.012);
          float tmpvar_28;
          tmpvar_28 = (tmpvar_25 - 0.012);
          if(((tmpvar_27>0) && (tmpvar_28>0)))
          {
              alpha_1 = pow(tmpvar_26, ((tmpvar_27 * tmpvar_28) * 200000));
          }
          float4 tmpvar_29;
          tmpvar_29 = tex2D(_MainTex, c1_3);
          float4 tmpvar_30;
          tmpvar_30 = (tmpvar_29 * alpha_1);
          out_f.color = tmpvar_30;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
