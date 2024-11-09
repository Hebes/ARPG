Shader "CameraFilterPack/Vision_Crystal"
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
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
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
          float2 p_1;
          float2 uv_2;
          float z_3;
          float3 c_4;
          float2 tmpvar_5;
          tmpvar_5.x = _Value2;
          tmpvar_5.y = _Value3;
          float2 tmpvar_6;
          tmpvar_6 = (in_f.xlv_TEXCOORD0 + tmpvar_5);
          c_4 = float3(0, 0, 0);
          p_1 = (tmpvar_6 - 0.5);
          z_3 = ((_Time * 20).x + 0.07);
          float tmpvar_7;
          tmpvar_7 = sqrt(dot(p_1, p_1));
          uv_2 = (tmpvar_6 + (((p_1 / tmpvar_7) * (sin(z_3) + _Value)) * abs(sin(((tmpvar_7 * 9) - (z_3 * 2))))));
          float2 tmpvar_8;
          tmpvar_8 = frac(abs(uv_2));
          float tmpvar_9;
          if((uv_2.x>=0))
          {
              tmpvar_9 = tmpvar_8.x;
          }
          else
          {
              tmpvar_9 = (-tmpvar_8.x);
          }
          float tmpvar_10;
          if((uv_2.y>=0))
          {
              tmpvar_10 = tmpvar_8.y;
          }
          else
          {
              tmpvar_10 = (-tmpvar_8.y);
          }
          float2 tmpvar_11;
          tmpvar_11.x = tmpvar_9;
          tmpvar_11.y = tmpvar_10;
          float2 tmpvar_12;
          tmpvar_12 = abs((tmpvar_11 - 0.5));
          c_4.x = (0.01 / sqrt(dot(tmpvar_12, tmpvar_12)));
          p_1 = (tmpvar_6 - 0.5);
          z_3 = (z_3 + 0.07);
          float tmpvar_13;
          tmpvar_13 = sqrt(dot(p_1, p_1));
          uv_2 = (tmpvar_6 + (((p_1 / tmpvar_13) * (sin(z_3) + _Value)) * abs(sin(((tmpvar_13 * 9) - (z_3 * 2))))));
          float2 tmpvar_14;
          tmpvar_14 = frac(abs(uv_2));
          float tmpvar_15;
          if((uv_2.x>=0))
          {
              tmpvar_15 = tmpvar_14.x;
          }
          else
          {
              tmpvar_15 = (-tmpvar_14.x);
          }
          float tmpvar_16;
          if((uv_2.y>=0))
          {
              tmpvar_16 = tmpvar_14.y;
          }
          else
          {
              tmpvar_16 = (-tmpvar_14.y);
          }
          float2 tmpvar_17;
          tmpvar_17.x = tmpvar_15;
          tmpvar_17.y = tmpvar_16;
          float2 tmpvar_18;
          tmpvar_18 = abs((tmpvar_17 - 0.5));
          c_4.y = (0.01 / sqrt(dot(tmpvar_18, tmpvar_18)));
          p_1 = (tmpvar_6 - 0.5);
          z_3 = (z_3 + 0.07);
          float tmpvar_19;
          tmpvar_19 = sqrt(dot(p_1, p_1));
          uv_2 = (tmpvar_6 + (((p_1 / tmpvar_19) * (sin(z_3) + _Value)) * abs(sin(((tmpvar_19 * 9) - (z_3 * 2))))));
          float2 tmpvar_20;
          tmpvar_20 = frac(abs(uv_2));
          float tmpvar_21;
          if((uv_2.x>=0))
          {
              tmpvar_21 = tmpvar_20.x;
          }
          else
          {
              tmpvar_21 = (-tmpvar_20.x);
          }
          float tmpvar_22;
          if((uv_2.y>=0))
          {
              tmpvar_22 = tmpvar_20.y;
          }
          else
          {
              tmpvar_22 = (-tmpvar_20.y);
          }
          float2 tmpvar_23;
          tmpvar_23.x = tmpvar_21;
          tmpvar_23.y = tmpvar_22;
          float2 tmpvar_24;
          tmpvar_24 = abs((tmpvar_23 - 0.5));
          c_4.z = (0.01 / sqrt(dot(tmpvar_24, tmpvar_24)));
          float4 tmpvar_25;
          tmpvar_25 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          c_4 = (c_4 + tmpvar_25.xyz);
          float4 tmpvar_26;
          tmpvar_26.w = 1;
          tmpvar_26.xyz = float3((c_4 / tmpvar_19));
          out_f.color = tmpvar_26;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
