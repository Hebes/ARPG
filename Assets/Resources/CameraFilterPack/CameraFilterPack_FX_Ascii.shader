Shader "CameraFilterPack/FX_Ascii"
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
      uniform float4 _ScreenResolution;
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
          float n_1;
          float3 col_2;
          float2 tmpvar_3;
          tmpvar_3 = (in_f.xlv_TEXCOORD0 * _ScreenResolution.xy);
          float2 P_4;
          P_4 = ((floor((tmpvar_3 / 8)) * 8) / _ScreenResolution.xy);
          float3 tmpvar_5;
          tmpvar_5 = tex2D(_MainTex, P_4).xyz;
          col_2 = tmpvar_5;
          float tmpvar_6;
          tmpvar_6 = ((col_2.x + col_2.z) / 2);
          n_1 = 65536;
          if((tmpvar_6>0.2))
          {
              n_1 = 65600;
          }
          if((tmpvar_6>0.3))
          {
              n_1 = 332772;
          }
          if((tmpvar_6>0.4))
          {
              n_1 = 15255090;
          }
          if((tmpvar_6>0.5))
          {
              n_1 = 23385160;
          }
          if((tmpvar_6>0.6))
          {
              n_1 = 15252010;
          }
          if((tmpvar_6>0.7))
          {
              n_1 = 13199450;
          }
          if((tmpvar_6>0.8))
          {
              n_1 = 11512810;
          }
          float2 tmpvar_7;
          tmpvar_7 = ((tmpvar_3 / 4) / float2(2, 2));
          float2 tmpvar_8;
          tmpvar_8 = (frac(abs(tmpvar_7)) * float2(2, 2));
          float tmpvar_9;
          if((tmpvar_7.x>=0))
          {
              tmpvar_9 = tmpvar_8.x;
          }
          else
          {
              tmpvar_9 = (-tmpvar_8.x);
          }
          float tmpvar_10;
          if((tmpvar_7.y>=0))
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
          float2 p_12;
          p_12 = (tmpvar_11 - 1);
          int tmpvar_13;
          tmpvar_13 = int(1);
          float tmpvar_14;
          float2 tmpvar_15;
          tmpvar_15 = floor(((p_12 * float2(4, (-4))) + 2.5));
          p_12 = tmpvar_15;
          float tmpvar_16;
          tmpvar_16 = clamp(tmpvar_15.x, 0, 4);
          int tmpvar_17;
          if((tmpvar_16==tmpvar_15.x))
          {
              float tmpvar_18;
              tmpvar_18 = clamp(tmpvar_15.y, 0, 4);
              tmpvar_17 = (tmpvar_18==tmpvar_15.y);
          }
          else
          {
              tmpvar_17 = int(0);
          }
          if(tmpvar_17)
          {
              float tmpvar_19;
              tmpvar_19 = ((n_1 / exp2((tmpvar_15.x + (5 * tmpvar_15.y)))) / 2);
              float tmpvar_20;
              tmpvar_20 = (frac(abs(tmpvar_19)) * 2);
              float tmpvar_21;
              if((tmpvar_19>=0))
              {
                  tmpvar_21 = tmpvar_20;
              }
              else
              {
                  tmpvar_21 = (-tmpvar_20);
              }
              if((int(tmpvar_21)==1))
              {
                  tmpvar_14 = 1;
                  tmpvar_13 = int(0);
              }
          }
          if(tmpvar_13)
          {
              tmpvar_14 = 0;
              tmpvar_13 = int(0);
          }
          col_2 = (col_2 * tmpvar_14);
          float4 tmpvar_22;
          tmpvar_22.w = 1;
          tmpvar_22.xyz = float3(col_2);
          out_f.color = tmpvar_22;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
