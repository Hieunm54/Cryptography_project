import React from 'react'
import BlockList from './Block/BlockList';
import Transaction from '../Container/Transaction/Transaction';

const Container = () => {

    return (
        <div className="container">
            <h2>Block on chain</h2>
            <p>Each card represent a block on the chain. You can see more details by clicking on a specific block</p>
            {/* <Block/>
             */}
            <BlockList />
            <Transaction />
        </div>
    )
}

export default Container
