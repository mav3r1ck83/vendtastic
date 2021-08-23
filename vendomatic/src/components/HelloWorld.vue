<template>
<v-container>
  <div class="mt=20;">
    <v-row style>
      <h3 style="margin: 18px">
        COINS INFORMATION
      </h3>
    </v-row>
    <v-card>
    <v-row style="margin:10">
    <v-col style="margin:10">
      <v-text-field
      id ="penny"
      v-model="pennyAmount"
      label="Enter Penny"
      length
      ></v-text-field>
    </v-col>
    <v-col style="margin:10">
      <v-text-field
      id ="nickel"
      v-model="nickelAmount"
      label="Enter nickel"
      ></v-text-field>
    </v-col>
    <v-col style="margin:10">
      <v-text-field
      id ="dime"
      v-model="dimeAmmount"
      label="Enter Dime"
      ></v-text-field>
    </v-col>
    <v-col style="margin:10">
      <v-text-field
      id ="quarter"
      v-model="quarterAmmount"
      label="Enter Quarter"
      ></v-text-field>
    </v-col>
    </v-row>
    </v-card>
        <v-row style>
      <h3 style="margin: 25px">
        PRODUCT INFORMATION
      </h3>
    </v-row>
    <v-row>
      <v-col>
        <v-card>
      <v-text-field
      id ="Pepsi"
      v-model="pepsiBuy"
      label="Pepsi"
      ></v-text-field>
            <v-text-field
      id ="Coke"
      v-model="cokeBuy"
      label="Coke"
      ></v-text-field>
            <v-text-field
      id ="Soda"
      v-model="sodaBuy"
      label="Soda"
      ></v-text-field>
        </v-card>
      </v-col>
      <v-col>
        <v-card>
          <v-text-field
          disabled= true
          style="margin-top: 75px"
          id ="total"
          v-model="totalMoney"
          label="Money Entered"
          ></v-text-field>
          <v-text-field
          disabled="true"
          id ="total"
          v-model="totalDue"
          label="Total Due"
          ></v-text-field>
        </v-card>
        <v-btn
          :disabled="!readyPurchase"
          @click="purchaseNow"
          style="margin:20px; float:right; width: 150px"
          id="purchase">
          BUY
        </v-btn>
      </v-col>
    </v-row>
  </div>
  </v-container>
</template>

<script>
import axios from 'axios'
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data: function(){
    return{
      pennyAmount: 0,
      nickelAmount: 0,
      dimeAmmount: 0,
      quarterAmmount: 0,
      pepsiBuy: 0,
      cokeBuy:0,
      sodaBuy:0,
      total:0,
      pepsiCost:36,
      cokeCost:25,
      sodaCost:45,
      cokeAmmount: 0,
      pepsiAmmount: 0,
      sodaAmmount: 0,
    }
  },
  computed:{

    totalDue(){
      return ((this.pepsiBuy *  this.pepsiCost) + (this.cokeBuy * this.cokeCost) + (this.sodaBuy * this.sodaCost))/100
    },
    totalMoney(){
      return ((this.pennyAmount * .01) + (this.nickelAmount * .05) + (this.dimeAmmount * .10) + (this.quarterAmmount * .25)).toFixed(2)
    },
    positiveInput(){
      return (this.pennyAmount >= 0 && this.nickelAmount >= 0 && this.dimeAmmount >= 0 && this.quarterAmmount >= 0 && this.cokeBuy >= 0 && this.pepsiBuy >=0 && this.sodaBuy >=0)
    },
    supplyEnough(){
      return (this.cokeAmmount >= this.cokeBuy && this.pepsiAmmount >= this.pepsiBuy && this.sodaAmmount >= this.sodaBuy)
    },
    readyPurchase(){
      return (this.totalDue <= this.totalMoney && this.totalDue > 0 && this.positiveInput && this.supplyEnough)
    },

  },
  mounted: function(){
    var initialLoad = axios.get(`https://localhost:5001/vendOrama/VendOrama/`)
    initialLoad.forEach(element => {
       switch (element.productName){
         case 'Coke':
           this.cokeAmmount = element.productAmount
           this.cokeCost = element.productPrice
           break
          case 'Pepsi':
            this.pepsiAmmount = element.productAmount
            this.pepsiCost = element.productPrice
            break
          case 'Soda':
            this.sodaAmmount = element.productAmount
            this.sodaCost = element.productPrice
            break
          default:
            break
       }

    });
  },
  methods:{
    purchaseNow(){
      var params = `?pennyUsed=${this.pennyAmount}&dimeUsed=${this.dimeAmmount}&nickeUsed=${this.nickelAmount}&quarterUsed=${this.quarterAmmount}&pepsiPurchased=${this.pepsiBuy}&cokePurchased=${this.cokeBuy}&sodaPurchased=${this.sodaBuy}&paidAmount=${this.totalMoney}&amountDue=${this.totalDue}`
      var request =`https://localhost:5001/vendOrama/VendOrama/${params}`
      var newPurchase = axios.patch(request)
      if (newPurchase.purchaseSuccess){
        this.pennyAmount= newPurchase.pennyAmount
        this.nickelAmount = newPurchase.nickelAmount
        this.dimeAmmount = newPurchase.dimeAmmount
        this.quarterAmmount = newPurchase.quarterAmmount
        newPurchase.currentStock.forEach(element => {
          switch (element.productName){
          case 'Coke':
            this.cokeAmmount = element.productAmount
            this.cokeCost = element.productPrice
            break
            case 'Pepsi':
              this.pepsiAmmount = element.productAmount
              this.pepsiCost = element.productPrice
              break
            case 'Soda':
              this.sodaAmmount = element.productAmount
              this.sodaCost = element.productPrice
              break
            default:
              break
       }
        });
      } else{
        alert(newPurchase.errorMessage)
      }

    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
