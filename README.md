# Procedure Terrain Generation

This is a Unity3D project for 3D terrain generation through voxels.
I want to learn all about terrain generation, so I started with this project to learn something about it.
I'd like to achieve a usefull terrain generator for our games that allow us to build any kind of terrain we want or imagine.

This project is just starting !

### How It Works
This terrain generator uses densities over the space to define volumes that should be rendered or not. We can start with any base dentity, such a flat dentity to define the ground level of our new world.
To improve this base density for generating more complex enviroments, the generator uses 3D samples (3D textures). Applying those samples to our enviroment will result in atractive dentities that help to render different kinds of surfaces.

### Firsts Results

_Render of some 3D sample (Perlin Noise)_
![terra1](https://cloud.githubusercontent.com/assets/11860033/11261913/f97c5594-8e57-11e5-8640-97c5e24d50aa.jpg)

_Small plateau_
![plateau1](https://cloud.githubusercontent.com/assets/11860033/11261919/01e2d9ec-8e58-11e5-84a9-b4265c7d32e7.jpg)

_Small plateau with another ground density sample_
![plateau cliff](https://cloud.githubusercontent.com/assets/11860033/11261920/049ad130-8e58-11e5-9bc8-bf3b4347d4c2.jpg)

