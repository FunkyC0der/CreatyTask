Shader "Unlit/PaintTexture"
{
    Properties
    {
    	_MainTex ("Texture", 2D) = "white" {}
		_Size ("Size", Range(0, 1)) = 0.1
    	_Color ("Color", Color) = (1, 0, 0, 1)
    	[Toggle] _UseTexture ("Use Paint Texture", Int) = 0
	}
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float2 _Position;
			float _Size;
			float4 _Color;
			sampler2D _PaintTex;
			int _UseTexture;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 paintColor = _UseTexture == 1 ? tex2D(_PaintTex, i.uv) : _Color;
				
				return distance(i.uv, _Position) < _Size ? paintColor : col;
			}
			ENDCG
        }
    }
}