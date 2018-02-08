Shader "Custom/TimeFieldShader" {
	Properties {
		_TintColor ("Tint Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Transparency ("Transparency", Range (0.0, 0.5)) = 0.25
	}

	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				float vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			fixed4 _TintColor;
			float _Transparency;

			v2f vert ( appdata v ) {
				v2f o;
				/o.vertex = UnityObjectToClipPos ( v.vertex );
				o.uv = TRANSFORM_TEX ( v.uv, _MainTex );
				return o;
			}

			fixed4 frag ( v2f i ) : SV_Trget {
				fixed4 col = text2D ( _MainTex , i.uv ) + _TintColor;
				col.a = _Transparency;
				return col;
			}
			ENDCG
		}
	}
}
