<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <div class="cross" @click="close()"></div>
      </div>
      <div class="modal__container__content">
        <div class="row">
          <vTitle class="fieldTitle">Рег №</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Название ТС</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Номер ГИБДД</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Цвет</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Тип ТС</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Забор</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Объем</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Сжат</vTitle>
          <input type="text" placeholder="Значение не задано" name="" />
        </div>
        <div class="rowSelect">
          <vTitle class="fieldTitle">Организация</vTitle>
          <div class="select">
            <div @click="showOrganizations = !showOrganizations">
              <input 
                type="text" 
                name="organization"
                v-model.trim="organization"
                placeholder="Значение не задано"
                disabled
              />
              <div class="deleteSelected" v-show="organization" @click="organization = ''"></div>
            </div>
            <div class="options" v-show="showOrganizations">
              <div 
                class="option" 
                v-for="organization in organizations" 
                :key="organization.id"
                @click="chooseOrganization(organization.id, organization.name)"
              >
                <vTitle class="fieldTitle">{{ organization.name }}</vTitle>
              </div>
            </div>
          </div>
        </div>
        <div class="modal__container__content__saveBtn">
          <div class="saveBtn">
            <vButton class="greenButton">Поиск</vButton>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

export default {
  name: "searchVehicles",

  components: {
    vTitle,
    vButton,
  },

  data() {
    return {
      organizations: [
        {
          id: 1,
          name: 'Зеленый город'
        },
        {
          id: 2,
          name: 'Красная стрела'
        }
      ],
      showOrganizations: false,
      organization: '',
    }
  },

  methods: {
    close() {
      this.$emit('close')
    },

    chooseOrganization(id, name) {
      this.organization = name
      this.organizationId = id
      this.showOrganizations = false
    },
  }
}
</script>

<style lang="scss" scoped>
.modal {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.2);
  display: flex;
  align-items: center;
  justify-content: center;

  &__container {
    width: 600px;
    height: fit-content;
    background: #FFF;
    border: 1px solid #000;
    padding: 20px 10px 60px 10px;

    &__close {
      display: flex;
      justify-content: flex-end;
      align-items: center;

      .cross {
        width: 30px;
        height: 30px;
        position: relative;
        border-radius: 15px;
        border: 1px solid #000;
        cursor: pointer;

        &:hover {
          animation-name: rotate;
          animation-duration: 0.3s;

          @keyframes rotate {
            0% {
              transform: rotate(0deg);
            }

            50% {
              transform: rotate(45deg) scale(1.05);
            }

            100% {
              transform: rotate(90deg) scale(1.0);
            }
          }
        }

        &:after, &:before {
          content: '';
          width: 2px;
          height: 20px;
          position: absolute;
          top: 4px;
          left: 13px;
          background: #000;
        }

        &:after {
          transform: rotate(45deg);
        }

        &:before {
          transform: rotate(-45deg);
        }
      }
    }

    &__content {

      .rowSelect {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin: 30px 0;

        .select {
          width: 100%;
          margin-left: 20px;
          position: relative;
          cursor: pointer;

          &:hover .deleteSelected {
            display: block;
          }

          .deleteSelected {
            display: none;
            position: absolute;
            top: 6.5px;
            right: 10px;
            z-index: 100;
            transition: all .2s ease;

            @media screen and (min-width: 1441px) {
              top: 8px;
            }

            &:after, &:before {
              content: '';
              width: 2px;
              position: absolute;
              height: 10px;
              background: #000;

              @media screen and (min-width: 1441px) {
                height: 15px;
              }
            }

            &:after {
              transform: rotate(45deg);
            }

            &:before {
              transform: rotate(-45deg);
            }
          }

          input {
            border: 1px solid #000;
            width: 100%;
            height: 30px;
            padding: 10px 5px;
            font-family: 'Lato', sans-serif;
            font-size: 18px;
            line-height: 22px;
            font-weight: 400;
            color: #000;
            border-radius: 4px;
            cursor: pointer;

            @media screen and (max-width: 1440px) {
              height: 20px;
              font-size: 12px;
              line-height: 16px;
            }

            &::placeholder {
              font-size: 16px;
            }
          }

          .options {
            position: absolute;
            top: 35px;
            left: 0;
            width: 100%;
            padding: 5px 10px;
            border: 1px solid #000;
            background: #FFF;

            @media screen and (max-width: 1440px) {
              top: 25px;
            }
          }
        }
      }

      .row {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin: 30px 0;

        input {
          border: 1px solid #000;
          width: 100%;
          height: 30px;
          margin-left: 20px;
          padding: 10px 5px;
          font-family: 'Lato', sans-serif;
          font-size: 18px;
          line-height: 22px;
          font-weight: 400;
          color: #000;
          border-radius: 4px;

          @media screen and (max-width: 1440px) {
            height: 20px;
            font-size: 12px;
            line-height: 16px;
          }

          &::placeholder {
            font-size: 16px;
          }
        }
      }

      &__saveBtn {  
        display: flex;
        justify-content: flex-end;
        align-items: center;

        .saveBtn {
          width: 90px;
          height: 30px;
        }
      }
    }
  }
}
</style>
