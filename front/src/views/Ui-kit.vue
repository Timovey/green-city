<template>
  <div class="ui-kit">
    <div class="container">
      <vTitle class="pageTitle">UI-kit</vTitle>
    </div>
    <div class="container">
      <div class="button__container">
        <vButton class="greenButton"/>
      </div>
    </div>
    <div class="container">
      <vTitle class="pageTitle"/>
    </div>
    <div class="container">
      <div class="dropDownMenu__container">
        <dropDownMenu />
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import vButton from "@/components/ui-kit/vButton";
import vTitle from "@/components/ui-kit/vTitle";
import dropDownMenu from "@/components/navigation/dropDownMenu";

export default {
  name: "Ui-kit",

  components: {
    vButton,
    vTitle,
    dropDownMenu,
  },
}
</script>

<style lang="scss" scoped>
  .container {
    margin: 20px 0;
    display: flex;
    justify-content: center;
  }

  .button__container {
    width: 200px;
    height: 50px;
  }

  .dropDownMenu__container {
    background: #25480F;
    opacity: 0.75;
    margin: 30px;
    width: fit-content;
  }
</style>
