<template>
  <button>
    <slot>Button</slot>
  </button>
</template>

<script>
export default {
  name: "vButton",
}
</script>

<style scoped lang="scss">
.greenButton {
  background: rgba(37, 72, 15, 0.58);
  width: 100%;
  height: 100%;
  color: #FFF;
  font-size: 12px;
  line-height: 14px;
  font-family: 'Lato', sans-serif;
  font-weight: 700;
  padding: 5px 10px;
}

.whiteToGreenButton {
  background: #FFF;
  width: 100%;
  height: 100%;
  color: #000;
  font-size: 36px;
  line-height: 38px;
  font-family: 'Lato', sans-serif;
  font-weight: 500;
  padding: 5px 10px;
  border: 1px solid #000000;
  border-radius: 10px;
  transition: all .2s ease;

  &:hover {
    background: rgba(37, 72, 15, 0.58);
    color: #FFF;
  }

  @media screen and (max-width: 1440px) {
    font-size: 30px;
    line-height: 32px;
  }
}

.rounded--10 {
  border-radius: 10px;
}

.size--32 {
  font-size: 26px;
  line-height: 28px;

  @media screen and (max-width: 1440px) {
    font-size: 20px;
    line-height: 22px;
  }
}
</style>
