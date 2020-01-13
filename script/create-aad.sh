#!/bin/bash

/Library/Frameworks/Mono.framework/Versions/Current/bin/msbuild awesome/awesome.csproj \
	-t:SignAndroidPackage \
	-p:Configuration=Release \
	-p:AndroidKeyStore=True \
	-p:AndroidSigningKeyStore=../milkcocoa.keystore \ # プロジェクトに対しての相対パス
	-p:AndroidSigningStorePass=koronAs0102054 \
	-p:AndroidSigningKeyAlias=milkcocoa \
	-p:AndroidSigningKeyPass=koronAs0102054
