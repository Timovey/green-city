<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Выбор контейнеров</vTitle>
        <div class="cross" @click="saveChoose()"></div>
      </div>
      <div class="modal__container__content">
        <div class="main__table">
          <div class="main__table__header">
            <div class="table__cell header small__cell"></div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">№</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Название</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">График</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Комментарий к графику</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Начало работы</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Конец работы</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Запись корректна</vTitle>
            </div>
            <div class="table__cell header big__cell">
              <vTitle class="tableCellTitle">Дата внесения в реестр</vTitle>
            </div>
          </div>
          <div class="main__table__header" v-for="(container, index) in containers" :key="index" :class="{isWarning: container.isInContract}">
            <div class="table__cell small__cell">
              <input type="checkbox" :id="'radioCheckedContainer_' + index" :checked="container.checked" name="radioSelect" class="radioBtn">
              <label :for="'radioCheckedContainer_' + index" @click.stop="chooseContainer(container.containerId)">
              <div class="typeRadio">
                  <div class="typeRadio__back"></div>
                </div>
              </label>
            </div>
            <div class="table__number table__cell">
              <vText class="tableCell">{{container.containerId}}</vText>
            </div>
            <div class="table__regNumber table__cell">
              <vText class="tableCell">{{container.containerName}}</vText>
            </div>
            <div class="table__regNumber table__cell" @click="openCronModal(container.containerId, container.cronShedule, container.containerName)">
              <input type="text" disabled placeholder="график">
            </div>
            <div class="table__regNumber table__cell">
              <input type="text" placeholder="комментарий" v-model="container.sheduleComment">
            </div>
            <div class="table__regNumber table__cell">
              <input type="text" v-model="container.workTimeFrom" v-mask="'99:99'" placeholder="__:__">
            </div>
            <div class="table__regNumber table__cell">
              <input type="text" v-model="container.workTimeTo" v-mask="'99:99'" placeholder="__:__">
            </div>
            <div class="table__regNumber table__cell">
              <input type="checkbox" :id="'radioCheckedisInRegistry_' + index" :checked="container.isInRegistry" name="radioSelect" class="radioBtn">
              <label :for="'radioCheckedisInRegistry_' + index" @click.stop="container.isInRegistry = !container.isInRegistry">
              <div class="typeRadio">
                  <div class="typeRadio__back"></div>
                </div>
              </label>
            </div>
            <div class="table__regNumber table__cell big__cell">
              <date-picker 
                v-model="container.inRegistryFromDate" 
                :lang="lang" 
                :format="format"
                valueType="format"
                input-class="datePickerInput"
                class="datePickerCreate"
              >
              </date-picker>
            </div>
          </div>
        </div>
      </div>
      <!-- <div class="modal__container__save">
        <div class="flexRow">
          <div @click="openCommonCronModal(cronCommon)">
            <input type="text" disabled placeholder="график">
          </div>
          <div>
            <input type="checkbox" :checked="commonChecked" @click="setCommonShedule">
          </div>
          <vText class="tableCell">График как у КП</vText>
        </div>
      </div> -->
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native.stop="saveChoose()">Выбрать</vButton>
        </div>
      </div>
    </div>
    <schedules v-if="showCommonCronModal" :cronProp="cronCommon" :common="true" @closeCommon="closeCommonCron" />
    <schedules v-if="showCronModal" :cronProp="cronForModal" :containerYard="choosenStand" @close="closeSchedules" :container="choosenContainer" :common="false" />
    <!-- <cronModal v-if="showCommonCronModal" :cronProp="cronCommon" :common="true" @closeCommon="closeCommonCron"/>
    <cronModal v-if="showCronModal" :cronProp="cronForModal" :containerYard="choosenStand" :container="choosenContainer" :common="false" @close="closeCronModal"/> -->
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';


import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import vButton from "@/components/ui-kit/vButton";

import schedules from "@/components/cron/schedules.vue";

import axios from 'axios';

export default {
  name: "containersListForContract",

  components: {
    vTitle,
    vText,
    vButton,
    DatePicker,
    schedules
  },

  data() {
    return {
      containers: [],
      createDate: '',
      commonCronModal: false,
      commonChecked: false,
      showCronModal: false,
      showCommonCronModal: false,
      choosenContainer: {
        id: null,
        address: null
      },
      cronCommon: {},
      cronForModal: {
        contractSchedule: {}
      },
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'YYYY-MM-DD',
    }
  },

  props: {
    choosenStand: {
      type: Object,
      default() {
        return {
          id: null,
          address: ""
        }
      } 
    },

    attached: {
      type: Array,
      default() {
        return []
      }
    }
  },

  created() {
    if (this.choosenStand.id) {
      this.getContainer()
    }
  },

  methods: {

    setCommonShedule() {
      this.commonChecked = !this.commonChecked
      if (this.commonChecked) {
        this.containers.forEach((container) => {
          if (!container.isInContract) {
            container.checked = true
            container.cronShedule = this.cronForModal
          }
        })
      }
    },

    closeSchedules(cron, containerId) {
      this.containers.forEach(container => {
        if (container.containerId == containerId) {
          container.cronShedule = cron
        }
      })
      this.showCronModal = false
    },

    closeCommonCron(cron) {
      if (this.commonChecked) {
        this.containers.forEach(container => {
          container.cronShedule.contractSchedule = cron
        })
      }
      this.showCommonCronModal = false
    },

    openCommonCronModal(cron) {
      if (cron.length) {
        this.cronForModal.contractSchedule = cron
      }
      this.showCommonCronModal = true
    },

    openCronModal(id, cron, address) {
      if (cron) {
        delete cron.contractSchedule
      }
      if (cron !== '') {
        console.log(cron)
        this.cronForModal = cron
      }
      this.showCronModal = true
      this.choosenContainer.id = id
      this.choosenContainer.address = address
    },

    close(param = []) {
      this.$emit('close', param)
    },

    chooseContainer(id) {
      for (let i = 0; i < this.containers.length; i++) {
        if (this.containers[i].containerId == id) {
          this.containers[i].checked = !this.containers[i].checked
        }
      }
    },

    saveChoose() {
      this.containers.forEach((container) => {
        if (!container.checked) {
          container.cronShedule = ''
          container.sheduleComment = ''
        }
      })
      this.close(this.containers)
    },

    async getContainer() {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          standIds: [Number(this.choosenStand.id)]
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetStand`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          this.containers = response.data.data[0].attachedContainers
          if (this.attached.length) {
            this.attached.forEach((container) => {
              this.containers.forEach((c) => {
                if (container.containerId == c.containerId) {
                  c.checked = container.checked
                  if (container.cronShedule != "") {
                    c.cronShedule = container.container2Schedule.contractSchedule
                  }
                  else {
                    c.cronShedule = {}
                  }
                  c.sheduleComment = container.sheduleComment
                  c.isInContract = container.isInContract
                  c.workTimeFrom = container.workTimeFrom
                  c.workTimeTo = container.workTimeTo
                  c.isInRegistry = container.isInRegistry
                  c.inRegistryFromDate = container.inRegistryFromDate
                }
              })
            })
          }
          else {
            this.containers.forEach((container) => {
              container.checked = false
              container.cronShedule = {}
              container.sheduleComment = ''
              container.workTimeFrom = ''
              container.workTimeTo = ''
              container.isInRegistry = false
              container.inRegistryFromDate = ''
            })
          }
        })
        .catch((error) => {
          console.log(error);
        })
    },

  },

  directives: {
    ClickOutside
  }
}
</script>

<style lang="scss">
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

  .radioBtn {
    display: none;

    &:checked + label .typeRadio__back {
      background: rgba(37, 72, 15, 0.58);
    }
  }

  .typeRadio {
    cursor: pointer;
    border: 1px solid #000;
    width: 16px;
    height: 16px;
    border-radius: 15px;
    display: flex;
    justify-content: center;
    align-items: center;

    &__back {
      width: 10px;
      height: 10px;
      border-radius: 15px;     
    }
  }

  &__container {
    width: 1000px;
    height: fit-content;
    background: #FFF;
    border: 1px solid #000;
    padding: 20px 10px 40px 10px;

    &__save {
      margin-top: 20px;
      width: 100%;
      display: flex;
      justify-content: flex-end;
      align-items: center;

      .row {
        width: 100px;
      }

      .flexRow {
        display: flex;
        width: 200px;
        justify-content: space-between;
        align-items: center;

        input[type="text"] {
          width: 70px;
        }
      }
    } 

    &__close {
      display: flex;
      justify-content: space-between;
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
      .main {

        &__table {
          margin-top: 10px;
          box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
          max-height: 700px;
          overflow-y: scroll;
          height: fit-content;

          .isWarning {
            background: #fcdd6f;
          }

          &__header {
            display: flex;
            width: 100%;
            justify-content: space-between;
            align-items: stretch;

            .small__cell {
              width: 40px !important;
              text-align: center;
            }

            .big__cell {
              width: 170px !important;
              text-align: center;
            }

            .table__cell {
              padding: 8px 10px;
              border: 1px solid #A5C081;
              width: calc((100% / 7) - 210px/7);

              input {
                background: inherit;
                color: #000;
                width: 100%;

                &::placeholder {
                  color: #000;
                }
              }

              .datePickerCreate {
                width: 100%;
              }

              .datePickerInput {
                font-family: 'Roboto', sans-serif;
                font-weight: 700;
                font-size: 12px;
                line-height: 15px;
                color: #000000;
                display: inline-block;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
                width: 100px;
                color: #000;
                height: 20px;
                padding: 6px 10px;
                padding-left: 10px;
                background-color: #fff;
                border-radius: 4px;
                -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
                box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
                cursor: pointer;
              }
            }

            .table__shedule {
              display: flex;
              align-items: center;
              justify-content: flex-start;

              input {
                border: 1px solid #000;
                width: 50%;
                border-radius: 5px;
                height: 30px;
                padding: 5px;
                margin-right: 20px;
              }
            }

            .header {
              background: rgba(0, 0, 0, 0.05);
            }
          }
        }
      }
    }
  }
}
</style>
