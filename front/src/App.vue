<template>
  <div id="app">
    <!-- <div id="nav">
      <router-link to="/">Home</router-link> |
      <router-link to="/ui-kit">UI-kit</router-link>
    </div> -->
    <router-view />
  </div>
</template>

<style lang="scss">
@import url('https://fonts.googleapis.com/css2?family=Lato:wght@400;700&display=swap');
@import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap');

*{padding:0;margin:0;border:0;}
*,*:before,*:after{-moz-box-sizing:border-box;-webkit-box-sizing:border-box;box-sizing: border-box;}
:focus,:active{outline: none;}
a:focus,a:active{outline: none; text-decoration: none;}
nav,footer,header,aside{display: block;}
html,body{overflow-x: hidden;}
input,button,textarea{font-family:inherit;}
input::-ms-clear{display: none;}
button{cursor: pointer;}
button::-moz-focus-inner{padding: 0;border:0;}
a{text-decoration: none;}
a:visited{text-decoration: none;}
a:hover{text-decoration: none;}
ul li{list-style: none;}
img{vertical-align: top;}
h1,h2,h3,h4,h5,h6{font-size: inherit;font-weight: inherit;}
</style>
