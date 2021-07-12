import React from 'react'
import BlockList from './Block/BlockList';

const Container = () => {

    return (
        <div className="container">
            <h2>Block on chain</h2>
            <p>Each card represent a block on the chain. You can see more details by clicking on a specific block</p>
            {/* <Block/>
             */}
             <BlockList/>
        </div>
    )
}

export default Container
