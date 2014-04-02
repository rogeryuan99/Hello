Shader "lavaspark/Double Colored Grayscale" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {} 
		_GrayScaleFactor("GrayScaleFactor", Range(0,1)) = 1
	}

	SubShader { 
		Lighting Off 
		Cull Off
		ZWrite Off 
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha 
		CGPROGRAM
		#pragma surface surf Unlit
			
		sampler2D _MainTex;
		
		half4 LightingUnlit (SurfaceOutput s, half3 lightDir, half atten) {
		    half4 c;
		    c.rgb = s.Albedo;
		    c.a = s.Alpha;
		    return c;
		}
		  
		struct Input {
			float2 uv_MainTex; 
			float4 color : COLOR;
		};
		float4 _Color;
		float _GrayScaleFactor;
		      
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);  
			o.Albedo = c.rgb*_Color.rgb*2*IN.color.rgb ;  
			if(_GrayScaleFactor > 0){
			   o.Albedo = o.Albedo.rgb*(1-_GrayScaleFactor) + _GrayScaleFactor*dot(o.Albedo.rgb, float3(0.3, 0.59, 0.11));
			}
			o.Alpha = c.a*IN.color.a;
		}
		ENDCG
	}
}