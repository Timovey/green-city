<template>
  <div class="toTopButton" @click="toTop">↑</div>
</template>

<script>
export default {
  name: 'toTopButton',
  props: {
    step: {// Эти данные контролируют скорость анимации
      type: Number,
      default: 50
    }
  },
  data() {
    return {
      isActive: false,
             showIndex: 0 // Показывать индекс по умолчанию
    }
  },

  mounted() {
    window.onscroll = this.magic
  },

  methods: {
    toTop() {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      });     
    },

    magic() {
      let btn = document.querySelector('.toTopButton')
      if (window.pageYOffset > 20) {
        btn.style.opacity = '1'
      } else { btn.style.opacity = '0' }
    },
  }
}
</script>

<style lang="scss">
  .toTopButton {
    opacity: 0;
    position: fixed;
    bottom: 10px;
    right: 10px;
    content: "";
    font-size: 30px;
    width: 40px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    background: rgba(37, 72, 15, 0.58);
    color: #FFF;
    border-radius: 20px;
    padding: 5px;
    cursor: pointer;
    transition: all .2s ease;
  }
</style>