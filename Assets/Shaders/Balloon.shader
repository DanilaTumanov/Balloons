Shader "Custom/Balloon" {
	Properties {
		_MainTex ("Main texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_BrightPower ("Bright Power", Range(0,1)) = 0.5
		_StartHeight ("Brightness start height", Float) = 0
	}
	SubShader {
		Tags { "Queue"="Transparent" }

		CGPROGRAM
		
		#pragma surface surf Lambert alpha

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		sampler2D _MainTex;
		fixed4 _Color;
		fixed _BrightPower;
		fixed _StartHeight;


		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * (1 + max(0, (IN.worldPos.y - _StartHeight) * _BrightPower));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
