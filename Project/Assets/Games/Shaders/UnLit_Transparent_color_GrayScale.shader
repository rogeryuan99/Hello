Shader "Unlit/Transparent_color_GrayScale" {

Properties {
	_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_GrayFactor ("_GrayFactor", Range(0,1) ) = 0	
}


SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 

	Pass {
		Lighting Off
		SetTexture [_MainTex] { combine texture } 
	}

	Pass {
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest 
		#include "UnityCG.cginc"
		
		uniform sampler2D _MainTex;
		float _GrayFactor;
		
		fixed4 frag (v2f_img i) : COLOR
		{
			fixed4 original = tex2D(_MainTex, i.uv);
			fixed grayscale = float4((original.r+original.g+ original.b)/3.0f,(original.r+original.g+ original.b)/3.0f,(original.r+original.g+ original.b)/3.0f, 1);
			original.rgb = lerp(original, float4(grayscale,grayscale,grayscale,original.a) , _GrayFactor);
			return original;
		}
		ENDCG
  }
}


}

