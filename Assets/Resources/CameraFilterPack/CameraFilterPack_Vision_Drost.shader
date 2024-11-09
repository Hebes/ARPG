Shader "CameraFilterPack/Vision_Drost"
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
          float4 src_1;
          float time_2;
          float3 draw_3;
          float atans_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          float vec_y_6;
          vec_y_6 = (uv_5.x - 0.5);
          float vec_x_7;
          vec_x_7 = (uv_5.y - 0.5);
          float tmpvar_8;
          float tmpvar_9;
          tmpvar_9 = (min(abs((vec_y_6 / vec_x_7)), 1) / max(abs((vec_y_6 / vec_x_7)), 1));
          float tmpvar_10;
          tmpvar_10 = (tmpvar_9 * tmpvar_9);
          tmpvar_10 = (((((((((((-0.01213232 * tmpvar_10) + 0.05368138) * tmpvar_10) - 0.1173503) * tmpvar_10) + 0.1938925) * tmpvar_10) - 0.3326756) * tmpvar_10) + 0.9999793) * tmpvar_9);
          tmpvar_10 = (tmpvar_10 + (float((abs((vec_y_6 / vec_x_7))>1)) * ((tmpvar_10 * (-2)) + 1.570796)));
          tmpvar_8 = (tmpvar_10 * sign((vec_y_6 / vec_x_7)));
          if((abs(vec_x_7)>(1E-08 * abs(vec_y_6))))
          {
              if((vec_x_7<0))
              {
                  if((vec_y_6>=0))
                  {
                      tmpvar_8 = (tmpvar_8 + 3.141593);
                  }
                  else
                  {
                      tmpvar_8 = (tmpvar_8 - 3.141593);
                  }
              }
          }
          else
          {
              tmpvar_8 = (sign(vec_y_6) * 1.570796);
          }
          atans_4 = ((tmpvar_8 + 3.141593) / 6.283185);
          time_2 = (_TimeX * _Value2);
          float2 tmpvar_11;
          tmpvar_11 = (((uv_5 - 0.5) * (-frac((time_2 + atans_4)))) + 0.5);
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, tmpvar_11);
          float4 tmpvar_13;
          tmpvar_13.xyz = tmpvar_12.xyz;
          tmpvar_13.w = float(((((tmpvar_11.x>=0) && (tmpvar_11.y>=0)) && (tmpvar_11.x<=1)) && (tmpvar_11.y<=1)));
          float4 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex, tmpvar_11);
          float4 tmpvar_15;
          tmpvar_15.xyz = tmpvar_14.xyz;
          tmpvar_15.w = float(((((tmpvar_11.x>=0) && (tmpvar_11.y>=0)) && (tmpvar_11.x<=1)) && (tmpvar_11.y<=1)));
          draw_3 = (tmpvar_13.xyz * tmpvar_15.www);
          float2 tmpvar_16;
          tmpvar_16 = (((uv_5 - 0.5) * (1 - frac((time_2 + atans_4)))) + 0.5);
          float4 tmpvar_17;
          tmpvar_17 = tex2D(_MainTex, tmpvar_16);
          float4 tmpvar_18;
          tmpvar_18.xyz = tmpvar_17.xyz;
          tmpvar_18.w = float(((((tmpvar_16.x>=0) && (tmpvar_16.y>=0)) && (tmpvar_16.x<=1)) && (tmpvar_16.y<=1)));
          float4 tmpvar_19;
          tmpvar_19 = tex2D(_MainTex, tmpvar_16);
          float4 tmpvar_20;
          tmpvar_20.xyz = tmpvar_19.xyz;
          tmpvar_20.w = float(((((tmpvar_16.x>=0) && (tmpvar_16.y>=0)) && (tmpvar_16.x<=1)) && (tmpvar_16.y<=1)));
          draw_3 = lerp(draw_3, tmpvar_18.xyz, tmpvar_20.www);
          float2 tmpvar_21;
          tmpvar_21 = (((uv_5 - 0.5) * (2 - frac((time_2 + atans_4)))) + 0.5);
          float4 tmpvar_22;
          tmpvar_22 = tex2D(_MainTex, tmpvar_21);
          float4 tmpvar_23;
          tmpvar_23.xyz = tmpvar_22.xyz;
          tmpvar_23.w = float(((((tmpvar_21.x>=0) && (tmpvar_21.y>=0)) && (tmpvar_21.x<=1)) && (tmpvar_21.y<=1)));
          float4 tmpvar_24;
          tmpvar_24 = tex2D(_MainTex, tmpvar_21);
          float4 tmpvar_25;
          tmpvar_25.xyz = tmpvar_24.xyz;
          tmpvar_25.w = float(((((tmpvar_21.x>=0) && (tmpvar_21.y>=0)) && (tmpvar_21.x<=1)) && (tmpvar_21.y<=1)));
          draw_3 = lerp(draw_3, tmpvar_23.xyz, tmpvar_25.www);
          float2 tmpvar_26;
          tmpvar_26 = (((uv_5 - 0.5) * (3 - frac((time_2 + atans_4)))) + 0.5);
          float4 tmpvar_27;
          tmpvar_27 = tex2D(_MainTex, tmpvar_26);
          float4 tmpvar_28;
          tmpvar_28.xyz = tmpvar_27.xyz;
          tmpvar_28.w = float(((((tmpvar_26.x>=0) && (tmpvar_26.y>=0)) && (tmpvar_26.x<=1)) && (tmpvar_26.y<=1)));
          float4 tmpvar_29;
          tmpvar_29 = tex2D(_MainTex, tmpvar_26);
          float4 tmpvar_30;
          tmpvar_30.xyz = tmpvar_29.xyz;
          tmpvar_30.w = float(((((tmpvar_26.x>=0) && (tmpvar_26.y>=0)) && (tmpvar_26.x<=1)) && (tmpvar_26.y<=1)));
          draw_3 = lerp(draw_3, tmpvar_28.xyz, tmpvar_30.www);
          float4 tmpvar_31;
          tmpvar_31 = tex2D(_MainTex, uv_5);
          src_1 = tmpvar_31;
          float2 x_32;
          x_32 = (float2(0.5, 0.5) - uv_5);
          float tmpvar_33;
          tmpvar_33 = clamp(((sqrt(dot(x_32, x_32)) - _Value) / ((_Value - 0.25) - _Value)), 0, 1);
          float _tmp_dvx_102 = (1 - (tmpvar_33 * (tmpvar_33 * (3 - (2 * tmpvar_33)))));
          draw_3 = lerp(src_1.xyz, draw_3, float3(_tmp_dvx_102, _tmp_dvx_102, _tmp_dvx_102));
          float4 tmpvar_34;
          tmpvar_34.w = 1;
          tmpvar_34.xyz = float3(draw_3);
          out_f.color = tmpvar_34;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
