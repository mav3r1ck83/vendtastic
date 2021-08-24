<template>
  <div>
    <v-card>
      <h3 style="margin: 18px">
        RECEIPT
      </h3>
        <v-card-text>
            <h4>
                Ordered Items
            </h4>
            <ul>
            <li v-for="item in purchasedList.orderReturn" :key="item">
                {{item}}
            </li>
            </ul>
            <h4>
                {{amountDueMsg}}
            </h4>
            <h4>
                {{changeMsg}}
            </h4>
            <h4>
                List of change
            </h4>
            <ul>
            <li v-for="item in listChange" :key="item">
                {{item}}
            </li>
            </ul>
        </v-card-text>
        <v-card-actions> 
            <v-btn @click="okClicked">OK</v-btn>
        </v-card-actions>
    </v-card>
  </div>
</template>
<script>



export default {
  name: "DatasourceReports",

  props:{
      purchasedList: { type: Object, default: null },
      amountDue: { type: Number, default: 0},
      totalMoney: { type: Number, default: 0}
  },
  data() {
    return {
        change: 0,
        listChange: [],
        changeMsg: "",
        amountDueMsg: ""
    };
  },
 
  mounted(){
      console.log(this.purchasedList)
      console.log(this.totalMoney)
      console.log(this.amountDue)
      this.amountDueMsg = "Purchase Total: " + this.amountDue
      this.change = this.totalMoney 
      this.changeMsg = "Change: "+ this.change
      if (this.change > 0){
          if (this.purchasedList.pennyAmount > 0){
              this.listChange.push("Pennies: " + this.purchasedList.pennyAmount)
          }
          if (this.purchasedList.nickelAmount > 0){
              this.listChange.push("Nickels: " + this.purchasedList.nickelAmount)
          }
          if (this.purchasedList.dimeAmount > 0){
              this.listChange.push("Dimes: " + this.purchasedList.dimeAmount)
          }
          if (this.purchasedList.querterAmount > 0){
              this.listChange.push("Quarters: " + this.purchasedList.querterAmount)
          }
      }

  },

  methods: {
    okClicked() {
      this.$emit("cancel");
    },
  },
  computed: {
  },

};
</script>
<style>


</style>
