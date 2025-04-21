debug:
	msbuild -restore ObjectGrabber.sln /property:Configuration="Debug"

release:
	msbuild -restore ObjectGrabber.sln /property:Configuration="Release"

run:
	-pkill Human
	cp build/bin/output/ObjectGrabber.dll ~/.steam/steam/steamapps/common/"Human Fall Flat"/BepInEx/plugins/ObjectGrabber.dll
	(steam steam://rungameid/477160 &)

.PHONY: clean

clean:
	rm -rf build/obj
	rm -rf build/bin
