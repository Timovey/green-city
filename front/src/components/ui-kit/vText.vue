<template>
  <p>
    <slot>Text</slot>
  </p>
</template>

<script>
export default {
  name: "vText",
}
</script>

<style scoped lang="scss">
.addDataCircleButton {
  font-family: 'Lato', sans-serif;
  font-weight: 700;
  font-size: 38px;
  line-height: 38px;
  color: rgba(37, 72, 15, 0.58);;
}

.tableCell {
  font-family: 'Lato', sans-serif;
  font-size: 12px;
  line-height: 14px;
  font-weight: 500;
  color: #000;
}

.graphFormation {
  font-family: 'Lato', sans-serif;
  font-weight: 700;
  font-size: 24px;
  line-height: 29px;
  color: #000000;
}
</style>
