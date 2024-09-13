<template>
  <h1>
    <slot>Title</slot>
  </h1>
</template>

<script>
export default {
  name: "vTitle",
}
</script>

<style scoped lang="scss">
.pageTitle {
  font-family: 'Lato', sans-serif;
  font-size: 20px;
  line-height: 24px;
  font-weight: 700;
  color: #000;

  @media screen and (max-width: 1440px) {
    font-size: 14px;
    line-height: 18px;
  }
}

.pageTitleWhite {
  font-family: 'Lato', sans-serif;
  font-size: 20px;
  line-height: 24px;
  font-weight: 700;
  color: #FFF;

  @media screen and (max-width: 1440px) {
    font-size: 14px;
    line-height: 18px;
  }
}

.fieldTitle {
  font-family: 'Lato', sans-serif;
  font-size: 20px;
  line-height: 24px;
  font-weight: 700;
  color: #000;
  flex: none;

  @media screen and (max-width: 1440px) {
    font-size: 12px;
    line-height: 16px;
  }
}

.loginTitle {
  font-family: 'Lato', sans-serif;
  font-size: 64px;
  line-height: 77px;
  font-weight: 500;
  color: #FFF;
  text-align: center;

  @media screen and (max-width: 1440px) {
    font-size: 40px;
    line-height: 53px;
  }
}

.printFormTitle {
  font-family: 'Lato', sans-serif;
  font-size: 40px;
  line-height: 53px;
  font-weight: 500;
  color: #FFF;
  text-align: center;

  @media screen and (max-width: 1440px) {
    font-size: 30px;
    line-height: 43px;
  }
}

.dropDownMenuTitle {
  font-family: 'Lato', sans-serif;
  font-size: 14px;
  line-height: 17px;
  font-weight: 700;
  color: #FFF;

  @media screen and (max-width: 1440px) {
    font-size: 10px;
    line-height: 13px;
  }
}

.navigationTitle {
  font-family: 'Lato', sans-serif;
  font-size: 18px;
  line-height: 22px;
  font-weight: 700;
  color: #FFF;

  @media screen and (max-width: 1440px) {
    font-size: 12px;
    line-height: 16px;
  }
}

.tableCellTitle {
  font-family: 'Lato', sans-serif;
  font-size: 12px;
  line-height: 14px;
  font-weight: 700;
  color: #000;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  user-select: none;
}
</style>
