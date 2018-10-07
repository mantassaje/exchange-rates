import React from 'react';
import moment from 'moment';

class ExchangeRatesTable extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      error: null
    };
  }

  render() {
    return (
      <react-component>
        {this.renderInfo()}
        <table >
          <thead>
            <tr>
              <th>Currency</th>
              <th>Rate difference</th> 
              <th>Rate</th>
            </tr>
          </thead>
          <tbody>
            {this.props.exchangeRates.map(item => (
              <tr key={item.currency}>
                <td>{item.currency}</td>
                {this.renderRateDifference(item)}
                {this.renderRate(item)}
              </tr>
            ))}
          </tbody>
        </table>
      </react-component>
    );
  }

  renderInfo(){
    if(this.props.exchangeRates != null && this.props.exchangeRates.length > 0)
    {
      var dateTo = moment(this.props.exchangeRates[0].dateTo).format("YYYY-MM-DD");
      return <div className="tableInfo">{dateTo} compared with previous day</div>;
    }
  }

  renderRateDifference(item){
    if(item.rateDifference === 0)
    {
      return(
        <td>-</td>
      );
    }else{
      return(
        <td>{item.rateDifference} LTL</td>
      );
    }
  }

  renderRate(item){
    if(item.rateDifference === 0)
    {
      return(
        <td>
          {item.baseRate}
          <span> /{item.quantity} LTL</span> 
        </td>
      );
    }else{
      return(
        <td>
          <span>Changed from </span> 
          {item.olderRate} 
          <span> to </span> 
          {item.baseRate}
          <span> /{item.quantity} LTL</span> 
        </td>
      );
    }
  }
}

export default ExchangeRatesTable;