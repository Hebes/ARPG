Shader "CameraFilterPack/Blizzard"
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
      uniform float _TimeX;
      uniform float _Value;
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
          float4 col2_2;
          float t_3;
          float tx_4;
          float3 col_5;
          float2 uv_6;
          float2 uvx_7;
          uvx_7 = in_f.xlv_TEXCOORD0;
          uv_6.y = uvx_7.y;
          float3 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, uvx_7).xyz;
          col_5 = tmpvar_8;
          float tmpvar_9;
          tmpvar_9 = (_TimeX * _Value);
          float tmpvar_10;
          tmpvar_10 = (1 + ((tmpvar_9 * sin(tmpvar_9)) / 16));
          uv_6.x = (uvx_7.x + tmpvar_10);
          uv_6.x = (uv_6.x - (tmpvar_9 + (sin((uv_6.x + (tmpvar_9 / 16))) / 16)));
          uv_6.y = (uvx_7.y + tmpvar_9);
          uv_6.y = (uv_6.y + (((uv_6.x * tmpvar_10) / 16) / 2));
          float4 tmpvar_11;
          tmpvar_11 = tex2D(_MainTex2, uv_6);
          col2_2.x = tmpvar_11.x;
          uv_6.y = uvx_7.y;
          tx_4 = (tmpvar_9 / 2);
          t_3 = (1 + ((tx_4 * sin(tx_4)) / 4));
          uv_6.x = (uvx_7.x + t_3);
          uv_6.x = (uv_6.x - (tx_4 + (sin((uv_6.x + (tx_4 / 8))) / 8)));
          uv_6.y = (uvx_7.y + tx_4);
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex2, uv_6);
          col2_2.y = tmpvar_12.y;
          uv_6 = (uvx_7 * 2);
          t_3 = (1 + ((tmpvar_9 * sin(tmpvar_9)) / 2));
          uv_6.x = (uv_6.x + t_3);
          uv_6.x = (uv_6.x - (tmpvar_9 + (sin((uv_6.x + (tmpvar_9 / 12))) / 8)));
          uv_6.y = (uv_6.y + tmpvar_9);
          uv_6.y = (uv_6.y + ((uv_6.x * t_3) / 64));
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex2, uv_6);
          col2_2.z = tmpvar_13.z;
          uv_6 = (uvx_7 / 2);
          tx_4 = (tmpvar_9 / 3);
          t_3 = (1 + ((tx_4 * sin(tx_4)) / 3));
          uv_6.x = (uv_6.x + t_3);
          uv_6.x = (uv_6.x - (tx_4 + (sin((uv_6.x + (tx_4 / 6))) / 12)));
          uv_6.y = (uv_6.y + tx_4);
          float4 tmpvar_14;
          tmpvar_14 = tex2D(_MainTex2, uv_6);
          col2_2.w = (tmpvar_14.y * 2);
          col2_2.x = max((col2_2.x * sin((t_3 / 10))), 0);
          col2_2.z = max((col2_2.z * sin((2 + (t_3 / 64)))), 0);
          col_5 = (col_5 + (((col2_2.x + col2_2.y) + (col2_2.z + col2_2.w)) / 4));
          float4 tmpvar_15;
          tmpvar_15.w = 1;
          tmpvar_15.xyz = float3(col_5);
          tmpvar_1 = tmpvar_15;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
