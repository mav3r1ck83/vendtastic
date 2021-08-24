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
      :disabled= noPepsi
      @change="sodaAvailableCheck"
      id ="Pepsi"
      v-model="pepsiBuy"
      label="Pepsi"
      ></v-text-field>
      <h6>
      {{pepsiMsg}}
      </h6>
      <v-text-field
      @change="sodaAvailableCheck"
      :disabled = noCoke
      id ="Coke"
      v-model="cokeBuy"
      label="Coke"
      ></v-text-field>
      <h6>
      {{cokeMsg}}
      </h6>
      <v-text-field
      @change="sodaAvailableCheck"
      :disabled= noSoda
      id ="Soda"
      v-model="sodaBuy"
      label="Soda"
      ></v-text-field>
      <h6>
      {{sodaMsg}}
      </h6>
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
          :disabled=!readyPurchase
          @click="purchaseNow"
          style="margin:20px; float:right; width: 150px"
          id="purchase">
          BUY
        </v-btn>
      </v-col>
    </v-row>
  </div>
      <v-dialog
      v-if="purchaseComplete"
      v-model="purchaseComplete"
      width="200"
      height="600"
      persistent
    >
      <reciept
        id="recipt"
        :purchasedList="purchaseReciept"
        :amountDue="totalDue"
        :totalMoney="totalMoney"
        @cancel="purchaseComplete = false , refreshData()"
      ></reciept>
    </v-dialog>
  </v-container>
</template>

<script>
import axios from 'axios'
import Reciept from "./reciept"
export default {
  name: 'HelloWorld',
  components:{
    Reciept
  },
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
      initialLoad: null,
      purchaseComplete: false,
      purchaseReciept: null,
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
    noPepsi(){
      return (this.pepsiAmmount == 0)
    },
    noCoke(){
      return (this.cokeAmmount == 0)
    },
    noSoda(){
      return (this.sodaAmmount == 0)
    },
    sodaMsg(){
      return ("Soda Available: " + this.sodaAmmount + " at cost: " + (this.sodaCost / 100) )
    },
    pepsiMsg(){
      return ("Pepsi Available: " + this.pepsiAmmount + " at cost: " + (this.pepsiCost / 100) )
    },
    cokeMsg(){
      return ("Coke Available: " + this.cokeAmmount + " at cost: " + (this.cokeCost / 100) )
    }

  },
  watch:{
    pennyAmount(newValue){
      if (newValue < 0)
        this.pennyAmount = this.pennyAmount * -1
    },
    nickelAmount(newValue){
      if (newValue < 0){
        this.nickelAmount = this.nickelAmount * -1
      }
    },
    dimeAmmount(newValue){
      if (newValue < 0){
        this.dimeAmmount = this.dimeAmmount * -1
      }
    },
    quarterAmmount(newValue){
      if(newValue < 0) {
        this.quarterAmmount = this.quarterAmmount * -1
      }
    }
  },
  created: function(){
    this.loadUp()

  },
  methods:{
    refreshData(){
        var newPurchase = this.purchaseReciept
        this.pennyAmount= 0
        this.nickelAmount = 0
        this.dimeAmmount = 0
        this.quarterAmmount = 0
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
        this.cokeBuy = 0
        this.pepsiBuy = 0
        this.sodaBuy = 0
    },
    sodaAvailableCheck(){
      if (this.pepsiBuy > this.pepsiAmmount ){
        this.pepsiBuy = this.pepsiAmmount
        alert(`That amount is beyond current stock level, theres only ${this.pepsiAmmount} Pepsi left`)
        
      }
      if (this.cokeBuy > this.cokeAmmount){
        this.cokeBuy = this.cokeAmmount
        alert(`That amount is beyond current stock level, theres only ${this.cokeAmmount} Coke left`)

      }
      if (this.sodaBuy > this.sodaAmmount){
        this.sodaBuy = this.sodaAmmount
        alert(`That amount is beyond current stock level, theres only ${this.sodaAmmount} Soda left`)

      }
      if (this.pepsiBuy < 0 ){
        this.pepsiBuy = this.pepsiBuy * -1
        
      }
      if (this.cokeBuy <0){
        this.cokeBuy = this.cokeBuy * -1

      }
      if (this.sodaBuy <0 ){
        this.sodaBuy = this.sodaBuy * -1

      }
    },
    async loadUp(){
      await axios.get(`https://localhost:5001/vendOrama/VendOrama/`).then(response => (this.initialLoad = response))
    this.initialLoad.data.forEach(element => {
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
    async purchaseNow(){
      var totalCents = this.totalMoney * 100
      var totalDueCents = this.totalDue * 100
      var params = `?pennyUsed=${this.pennyAmount !=''? this.pennyAmount: 0}&dimeUsed=${this.dimeAmmount !=''? this.dimeAmmount: 0}&nickeUsed=${this.nickelAmount !=''? this.nickelAmount: 0}&quarterUsed=${this.quarterAmmount !=''? this.quarterAmmount: 0}&pepsiPurchased=${this.pepsiBuy !=''? this.pepsiBuy:0}&cokePurchased=${this.cokeBuy !=''? this.cokeBuy:0}&sodaPurchased=${this.sodaBuy !=''? this.sodaBuy:0}&paidAmount=${totalCents}&amountDue=${totalDueCents}`
      var request =`https://localhost:5001/vendOrama/VendOrama/${params}`
      var newPurchase = null
      await axios.patch(request).then(response => (newPurchase = response.data))
      if (newPurchase.purchaseSuccess){
        this.pennyAmount= newPurchase.pennyAmount
        this.nickelAmount = newPurchase.nickelAmount
        this.dimeAmmount = newPurchase.dimeAmount
        this.quarterAmmount = newPurchase.querterAmount
        
        this.purchaseReciept = newPurchase
        this.purchaseReciept.amountDue = this.totalDue
        this.purchaseComplete = true
 
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
