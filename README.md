# DepthMapFromStereo
Create depth map from stereo pair.
To use this program you need to have an xml file with the following format:



<?xml version="1.0" encoding="utf-8"?>
<ArrayOfPair>
  <Pair>
    <Image1>
      <Path>Img/1.jpg</Path>
    </Image1>
    <Image2>
      <Path>Img/2.jpg</Path>
    </Image2>
    <Properties>
      <FocalLength>40</FocalLength>
      <Distance>100</Distance>
      <TemplateSize>20</TemplateSize>
    </Properties>
  </Pair>
  <Pair>
    <Image1>
      <Path>Img/1.jpg</Path>
    </Image1>
    <Image2>
      <Path>Img/2.jpg</Path>
    </Image2>
    <Properties>
      <FocalLength>30</FocalLength>
      <Distance>100</Distance>
      <TemplateSize>20</TemplateSize>
    </Properties>
  </Pair>
</ArrayOfPair>
