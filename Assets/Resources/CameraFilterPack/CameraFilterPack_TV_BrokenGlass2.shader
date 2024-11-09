Shader "CameraFilterPack/TV_BrokenGlass2"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _MainTex2 ("Base (RGB)", 2D) = "white" {}
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
      uniform sampler2D _MainTex2;
      uniform float _Bullet_1;
      uniform float _Bullet_2;
      uniform float _Bullet_3;
      uniform float _Bullet_4;
      uniform float _Bullet_5;
      uniform float _Bullet_6;
      uniform float _Bullet_7;
      uniform float _Bullet_8;
      uniform float _Bullet_9;
      uniform float _Bullet_10;
      uniform float _Bullet_11;
      uniform float _Bullet_12;
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
          float3 col2_2;
          float col_3;
          float2 uv2_4;
          float2 uv_5;
          uv_5 = in_f.xlv_TEXCOORD0;
          uv2_4 = uv_5;
          uv_5 = (uv_5 / 2);
          float4 tmpvar_6;
          tmpvar_6 = tex2D(_MainTex2, uv_5);
          col_3 = (tmpvar_6.xyz * _Bullet_1).x;
          uv_5 = (uv_5 + float2(0.5, 0));
          float4 tmpvar_7;
          tmpvar_7 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_7.xyz * _Bullet_2).x);
          uv_5 = (uv_5 + float2(0, 0.5));
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_8.xyz * _Bullet_3).x);
          uv_5 = (uv_5 - float2(0.5, 0));
          float4 tmpvar_9;
          tmpvar_9 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_9.xyz * _Bullet_4).x);
          float4 tmpvar_10;
          tmpvar_10 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_10.xyz * _Bullet_5).y);
          uv_5 = (uv_5 + float2(0.5, 0));
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_11.xyz * _Bullet_6).y);
          uv_5 = (uv_5 + float2(0, 0.5));
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_12.xyz * _Bullet_7).y);
          uv_5 = (uv_5 - float2(0.5, 0));
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_13.xyz * _Bullet_8).y);
          float4 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_14.xyz * _Bullet_9).z);
          uv_5 = (uv_5 + float2(0.5, 0));
          float4 tmpvar_15;
          tmpvar_15 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_15.xyz * _Bullet_10).z);
          uv_5 = (uv_5 + float2(0, 0.5));
          float4 tmpvar_16;
          tmpvar_16 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_16.xyz * _Bullet_11).z);
          uv_5 = (uv_5 - float2(0.5, 0));
          float4 tmpvar_17;
          tmpvar_17 = tex2D(_MainTex2, uv_5);
          col_3 = (col_3 + (tmpvar_17.xyz * _Bullet_12).z);
          uv2_4 = (uv2_4 + (float2(col_3, col_3) / 4));
          float3 tmpvar_18;
          tmpvar_18 = tex2D(_MainTex, uv2_4).xyz;
          col2_2 = tmpvar_18;
          col2_2 = (col2_2 + float3(col_3, col_3, col_3));
          float4 tmpvar_19;
          tmpvar_19.w = 1;
          tmpvar_19.xyz = float3(col2_2);
          tmpvar_1 = tmpvar_19;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
